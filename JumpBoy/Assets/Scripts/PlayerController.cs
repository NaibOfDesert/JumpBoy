using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // to simplification
    [Header("Layers & Objects")]
    public LayerMask JumpUpgradeLayer;
    public LayerMask CoinLayer;
    public LayerMask GoldLayer;
    public Object GroundObject;

    // 
    [Header("Player Values")]
    public RaycastHit2D wallHitCheck;
    public float wallDistance;
    public bool isWallSliding;
    public float wallJumpBreakTime;
    public float wallSlideSpeed;

    [Header("Player Test Values")]

    public float Vertical;
    public float VerticalSpeed;
    public int jumpTimeCounter;
    public bool isCollisionGold;

    public string coinTag;
    public string goldTag;
    public string jumpUpgradeTag;


    [Header("Move")]
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] bool isFaceRight;
    private Vector2 moveValue;
    
    [Header("Jump")]
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float jumpWallTime = 1.0f;
    [SerializeField] bool isJump;
    [SerializeField] bool isJumpPosible;

    [Header("Masks")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isTouchGround = false;


    Rigidbody2D rigidbody2d; 
    BoxCollider2D boxCollider2d;
    Animator animator; 



    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {


    }

    // Update is called once per frame  
    void Update()
    {
        Jump(); //--
        FlipCheck();
        Move();
        Slide();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if (boxCollider2d.IsTouchingLayers(groundLayer))
            {
                // isTouchGround = false;
                rigidbody2d.velocity += new Vector2(0f, jumpForce);
            }
            else
            {
                return; // isTouchGround = true;
            }
        }
    }

    void Jump()
    {

    }
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void Move()
    {
        rigidbody2d.velocity = new Vector2(moveValue.x * moveSpeed, rigidbody2d.velocity.y);


        bool platerHasHorizontalSpeed = Mathf.Abs(rigidbody2d.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRun", !platerHasHorizontalSpeed);
        

    }

    void Slide()
    {

    }

    private void FlipCheck()
    {
        if (rigidbody2d.velocity.x > Mathf.Epsilon && isFaceRight == true) Flip();
        else if (rigidbody2d.velocity.x < -Mathf.Epsilon && isFaceRight == false) Flip();
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFaceRight = !isFaceRight;
    }

    /*
    private void GetPlayerPosition()
    {
        player.MoveHorizontal = Input.GetAxisRaw("Horizontal"); // (-1, 0, 1)
        player.MoveVertical = Input.GetAxisRaw("Vertical"); // (-1, 0, 1)
        
    }

    // Update is called for physics changes
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        MovePlayerHorizontal();
        MovePlayerVertical();
        FlipCheck();
       //  IsCanJump();
        WallSlidingCheck();
        
    }

    private void MovePlayerHorizontal() {
        // split for Players
        if (player.MoveHorizontal > 0.1f || player.MoveHorizontal < -0.1f)
        {
            player.Rigidbody2D.AddForce(new Vector2(player.MoveHorizontal * player.MoveSpeed, 0f), ForceMode2D.Impulse);
        }

    }  
    
    private void MovePlayerVertical() {
        // split for Players
        if (isCanJump && player.MoveVertical > 0.1f)
        {
            player.Rigidbody2D.AddForce(new Vector2(0f, player.MoveVertical * player.JumpForce), ForceMode2D.Impulse);

        }
    }



    // to simplification
    private void WallSlidingCheck()
    {
        if (player.IsFacingRight)
        {
            wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, GroundLayer);
            // Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.red);
        }
        else
        {
            wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, GroundLayer);
            // Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.red);
        }

        if (wallHitCheck && player.IsJumping )
        {
            isWallSliding = true;
        }
        else isWallSliding = false;

        if (isWallSliding) isCanJump = true;


        if (isWallSliding && (player.MoveVertical < 0.1f || player.MoveVertical > -0.1f)) 
        {
            // TimeCheck();
            // player.Rigidbody2D.AddForce(new Vector2(0f, player.MoveVertical * 0.5f), ForceMode2D.Impulse); 
            player.Rigidbody2D.velocity = new Vector2(player.Rigidbody2D.velocity.x, Mathf.Clamp(player.Rigidbody2D.velocity.y, wallSlideSpeed, float.MaxValue));

        }
         // transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y , wallSlideSpeed, float.MaxValue));


    }

    private void IsCanJump()
    {
        if (player.IsJumping) isCanJump = false;
        else isCanJump = true;
    }

    private void TimeCheck()
    {
        if (wallJumpTime < 0.1)
        {
            isCanJump = true;
            wallJumpTime += Time.deltaTime;
            wallJumpBreakTime = 0;
        }
        else {
            if (wallJumpBreakTime < 0.3)
            {
                isCanJump = false;
                wallJumpBreakTime += Time.deltaTime;
            }
            else wallJumpTime = 0;
        }
            
    }

    private void RunAnimation()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ground Collision
        if (collision.gameObject == GroundObject)
        {
            player.IsJumping = false;
        }

        // Coin Collision - adding Coin Value
        if (collision.gameObject.CompareTag(coinTag))
        {
            Destroy(collision.gameObject);
            player.CoinValue++;
            Debug.Log(player.CoinValue);
        }

        // JumpUpgrade Collision - improve jump force
        if (collision.gameObject.CompareTag(jumpUpgradeTag))
        {
            Destroy(collision.gameObject);
            player.JumpLevel++;
        }

        // Gold Collision - quit game
       if (collision.gameObject.CompareTag(goldTag))
        {
            isCollisionGold = true;
            Application.Quit();

        }
    }

    */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GroundObject)
        {
            isJump = false;
        }
        else isJump = true;
    }
}

