using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Layers & Objects")]
    [SerializeField] LayerMask JumpUpgradeLayer;
    [SerializeField] LayerMask CoinLayer;
    [SerializeField] LayerMask GoldLayer;
    [SerializeField] Object GroundObject;
    [SerializeField] Object SlidingObject;

    [Header("Player Values")]
    public float wallJumpBreakTime;
    public float wallSlideSpeed;

    [Header("Player Test Values")]
    public float VerticalSpeed;
    public int jumpTimeCounter;
    public bool isCollisionGold;

    [Header("Rules")]
    [SerializeField] float standardGravityScale = 5.0f;
    [SerializeField] float slidingGravityScale = 1.0f;
    [SerializeField] int jumpDistanceToDie = 10;
    [SerializeField] float levelLoadDelay = 2f;

    [Header("Player")]
    [SerializeField] bool isIdle = false;
    [SerializeField] bool wallHitCheck;
    [SerializeField] float wallDistance;
    [SerializeField] int position;
    [SerializeField] bool isDead = false;
    [SerializeField] string movementStatus = "start";
    [SerializeField] bool movementStatusChange;
    [SerializeField] GameObject enemyObject;

    [Header("Move")]
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] bool isFaceRight = true;
    [SerializeField] bool isMove = false;
    private Vector2 moveValue;

    [Header("Jump")]
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float jumpWallTime = 1.0f;
    [SerializeField] bool isJump = true;
    [SerializeField] bool isJumpPosible;
    [SerializeField] bool isJumpFirst = false;
    [SerializeField] bool isCanSlide;
    [SerializeField] int jumpPosition = 0;
    [SerializeField] float jumpDistanceForce = 0;
    [SerializeField] bool isWallSlide;

    [Header("Masks")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isTouchGround = false;


    Rigidbody2D rigidbody2d;
    BoxCollider2D boxCollider2d;
    PlayerAnimations playerAnimations;
    PlayerAudioController playerAudioController;

    [Header("GloblLight")]
    [SerializeField] GameObject globalLight;
    LightController lightController;

    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        lightController = globalLight.GetComponent<LightController>();
        playerAudioController = GetComponent<PlayerAudioController>();
    }
    void Start()
    {
        rigidbody2d.gravityScale = standardGravityScale;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        if (!isDead)
        {
            position = (int)transform.position.y;
            IsIdle();
            IsJump();
            Move();
            FlipCheck();
            WallHitCheck();
            IsWallSlide();
            Slide();
            playerAnimations.AnimationSwitch("isMovement");
        }
    }
    public Rigidbody2D GetRigidbody2D()
    {
        return rigidbody2d;
    }
    void IsIdle()
    {
        if (isMove || isJump || isWallSlide)
        {
            isIdle = false;

        }
        else
        {
            isIdle = true;
            MovementStatusCheck("Idle");
        }
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
            if (transform.position.y > jumpPosition) jumpPosition = (int)transform.position.y;
            MovementStatusCheck("Jump");
        }
    }
    public bool GetIsJump()
    {
        return isJump;
    }
    public int GetPosition()
    {
        return position; 
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
        isMove = (Mathf.Abs(rigidbody2d.velocity.x) > Mathf.Epsilon) && !isJump;
        if(isMove) MovementStatusCheck("Move");
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
    public bool GetIsSlide()
    {
        return isWallSlide; 
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
    private void IsWallSlide()
    {
        if (wallHitCheck && isJump && isCanSlide)
        {
            isWallSlide = true;
            MovementStatusCheck("Slide");
        }
        else
        {
            isWallSlide = false;
        }
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
        Debug.Log("You died!!!");
        isDead = true;
        lightController.SetLight("Dead");
        playerAnimations.AnimationSwitch("isDead");
        MovementStatusCheck("Dead");
        StartCoroutine(RestartGame());
    }
    public bool IsDead()
    {
        return isDead;
    }
    void MovementStatusCheck(string status)
    {
        if (movementStatus != status)
        {
            movementStatus = status;
            movementStatusChange = true;
            playerAudioController.PlayAudioEffect(movementStatus);
        }
        else
        {
            movementStatusChange = false;
        }
    }
    public string GetMovementStatus()
    {
        return movementStatus;
    }
    public bool GetMovementStatusChange()
    {
        return movementStatusChange;
    }
    private IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Debug.Log("load scene");
        SceneManager.LoadScene(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GroundObject)
        {
            isJump = false;

            if (((int)transform.position.y - jumpPosition) < -jumpDistanceToDie)
            {
                Die();
            }
            else
            {
                jumpDistanceForce = 0;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == SlidingObject)
        {
            isCanSlide = true;
        }

        if (collision.gameObject == enemyObject)
        {
            Die();
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
            isJump = true;
            MovementStatusCheck("Jump");
            jumpPosition = (int)transform.position.y;
            jumpDistanceForce = Mathf.Abs(moveValue.x);
        }
    }
    
}

