using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    public int score { get; private set; }

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int starValue)
    {
        score += starValue;
        scoreText.text = "X" + score.ToString();
    }
}
