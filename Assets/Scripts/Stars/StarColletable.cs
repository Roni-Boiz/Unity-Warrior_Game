using UnityEngine;

public class StarColletable : MonoBehaviour
{
    [SerializeField] private int starValue;

    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ScoreManager.instance.ChangeScore(starValue);
            SoundManager.instance.PlaySound(pickupSound);
            gameObject.SetActive(false);
        }
    }
}
