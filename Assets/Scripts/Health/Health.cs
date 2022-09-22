using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float invisibleDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathAudioClip;

    [Header("Hurt Sound")]
    [SerializeField] private AudioClip hurtAudioClip;


    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtAudioClip);
        }
        else
        {
            if(!dead)
            {
                GetComponent<PlayerMovement>().enabled = false;
                animator.SetTrigger("die");
                animator.SetBool("grounded", true);
                dead = true;
                SoundManager.instance.PlaySound(deathAudioClip);
            }
            
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invisibleDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(invisibleDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        this.GetComponent<PlayerMovement>().enabled = true;
        StartCoroutine(Invulnerability());

        //foreach (Behaviour component in components)
        //    component.enabled = true;
    }

}
