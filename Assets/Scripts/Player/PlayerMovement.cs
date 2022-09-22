using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Edge Jump")]
    [SerializeField] private float edgeJumpTime;
    private float edgeJumpCounter;

    [Header("Multiple Jump")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    private void Awake()
    {
        // reference for body and animator of player object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        wallJumpCooldown = 0;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        // flip the player
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }

        // Set animator parameter
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 3;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                edgeJumpCounter = edgeJumpTime;
                jumpCounter = extraJumps; 
            }
            else
            {
                edgeJumpCounter -= Time.deltaTime;
            }
        }

    }

    private void Jump()
    {
        if (edgeJumpCounter < 0 && !onWall() && jumpCounter <= 0) return;

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
        {
            WallJump();
        }
        else
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if(edgeJumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
                else
                {
                    if(jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            edgeJumpCounter = 0;
        }

    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return !onWall();
    }

}