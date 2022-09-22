using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float fireAttackCooldown;
    [SerializeField] private float swordAttackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballClip;
    [SerializeField] private AudioClip swordAttackClip;

    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownTimer > fireAttackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        if (Input.GetMouseButtonDown(0) && cooldownTimer > swordAttackCooldown && playerMovement.canAttack())
        {
            Attack2();
        }
        cooldownTimer +=Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballClip);
        animator.SetTrigger("fireballAttack");
        cooldownTimer = 0;

        fireballs[findFireBall()].transform.position = firePoint.position;
        fireballs[findFireBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void Attack2()
    {
        SoundManager.instance.PlaySound(swordAttackClip);
        animator.SetTrigger("swordAttack");
        cooldownTimer = 0;
    }

    private int findFireBall()
    {
        for(int i = 0; i< fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
