using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // to simplification
    [Header("Layers & Objects")]
    [SerializeField] LayerMask JumpUpgradeLayer;
    [SerializeField] LayerMask CoinLayer;
    [SerializeField] LayerMask GoldLayer;
    [SerializeField] Object GroundObject;
    [SerializeField] Object SlidingObject; 
    // 
    [Header("Player Values")]


    public bool isWallSliding;
    public float wallJumpBreakTime;
    public float wallSlideSpeed;

    [Header("Player Test Values")]

    // [HideInInspector] public float Vertical;
    public float VerticalSpeed;
    public int jumpTimeCounter;
    public bool isCollisionGold;

    public string coinTag;
    public string goldTag;
    public string jumpUpgradeTag;

    [Header("Rules")]
    [SerializeField] float standardGravityScale = 5.0f;
    [SerializeField] float slidingGravityScale = 1.0f;
    [SerializeField] int jumpDistanceToDie = 10;

    [Header("Player")]
    [SerializeField] public bool wallHitCheck;
    [SerializeField] float wallDistance;
    [SerializeField] int position;
    [SerializeField] bool isDead = false;


    [Header("Move")]
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] bool isFaceRight = true;
    [SerializeField] bool isMove = false;
    private Vector2 moveValue;
    
    [Header("Jump")]
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float jumpWallTime = 1.0f;
    [SerializeField] bool isJump;
    [SerializeField] bool isJumpPosible;
    [SerializeField] bool isJumpFirst = false;
    [SerializeField] bool isCanSlide; 
    [SerializeField] int jumpPosition = 0;

    [Header("Masks")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isTouchGround = false;


    Rigidbody2D rigidbody2d; 
    BoxCollider2D boxCollider2d;

    PlayerAnimations playerAnimations; 


    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
        playerAnimations = FindObjectOfType<PlayerAnimations>();

    }

    void Start()
    {
        rigidbody2d.gravityScale = standardGravityScale;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x + 2f, rigidbody2d.velocity.y);
    }

    // Update is called once per frame  
    void Update()
    {
        if (!isDead)
        {
            position = (int)transform.position.y;

            IsJump();
            Move();
            FlipCheck();
            WallHitCheck();
            IsWallSiliding();
            Slide();
            playerAnimations.AnimationSwitch("isMovement");
        }

    }
    
    public Rigidbody2D GetRigidbody2D()
    {
        return rigidbody2d;
    }
    void OnJump(InputValue value)
    {
        if (!isDead)
        {
            if (value.isPressed)
            {
                if (boxCollider2d.IsTouchingLayers(groundLayer))
                {
                    isJumpFirst = true;
                    rigidbody2d.velocity += new Vector2(0f, jumpForce);
                }
                else if (!boxCollider2d.IsTouchingLayers(groundLayer) && isJumpFirst == true)
                {
                    isJumpFirst = false;
                    rigidbody2d.velocity += new Vector2(0f, jumpForce);
                }
                else
                {
                    return;
                }
            }
        }
        
    }

    void IsJump()
    {
        if (!boxCollider2d.IsTouchingLayers(groundLayer))
        {
            isJump = true;

        }
        else
        {
            isJump = false;
        }
    }

    public bool GetIsJump()
    {
        return isJump; 
    }

    void OnMove(InputValue value)
    {
        if (!isDead)
        {
            moveValue = value.Get<Vector2>();

            if (moveValue.x > Mathf.Epsilon)
            {
                isFaceRight = true;

            }
            else if (moveValue.x < -Mathf.Epsilon)
            {
                isFaceRight = false;
            }
        }
    }

    void Move()
    {
        rigidbody2d.velocity = new Vector2(moveValue.x * moveSpeed, rigidbody2d.velocity.y);
        isMove = (Mathf.Abs(rigidbody2d.velocity.x) > Mathf.Epsilon);
    }

    public bool GetIsMove()
    {
        return isMove; 
    }

    void Slide()
    {
        if (isJump && wallHitCheck && (rigidbody2d.velocity.y < -Mathf.Epsilon) && isCanSlide)
        {
            rigidbody2d.gravityScale = slidingGravityScale;
        }
        else rigidbody2d.gravityScale = standardGravityScale; 
    }

    private void WallHitCheck()
    {
        if (isFaceRight)
        {
            wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.red);
        }
        else 
        {
            wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.red);
        }
    }

    private void IsWallSiliding()
    {
        if (wallHitCheck && isJump) isWallSliding = true;
        else isWallSliding = false;
    }
    private void FlipCheck()
    {
        if ((!isFaceRight && transform.localScale.x > Mathf.Epsilon) || (isFaceRight && transform.localScale.x < -Mathf.Epsilon)) Flip();
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void Die()
    {
        playerAnimations.AnimationSwitch("isDead");
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GroundObject)
        {
            if (Mathf.Abs((int)transform.position.y - jumpPosition) > jumpDistanceToDie)
            {
                isDead = true;
                Die();
                Debug.Log("You died!!!");   
                // restart game
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject == SlidingObject)
        {
            isCanSlide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == SlidingObject)
        {
            isCanSlide = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GroundObject)
        {
            jumpPosition = (int)transform.position.y;
            isJump = false;
        }
        else isJump = true;
    }
}

