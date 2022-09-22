using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    public RectTransform rect;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        currentPosition = 0;
        ChangePosition(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        AssignPosition();
    }
    private void AssignPosition()
    {
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y);
    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
