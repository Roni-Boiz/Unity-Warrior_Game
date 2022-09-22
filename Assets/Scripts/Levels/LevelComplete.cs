using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip advancedSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (ScoreManager.instance.score == 5)
            {
                animator.SetTrigger("win");
                SoundManager.instance.PlaySound(advancedSound);
                StartCoroutine(WaitforSec());
            }
            else
            {
                //StartCoroutine("WaitforSec");
            }
            
        }
    }

    private IEnumerator WaitforSec()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
