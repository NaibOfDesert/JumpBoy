using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Wall Jump")]
    public LayerMask GroundLayer;
    public Object GroundObject;
    public float wallDistance;
    public bool isWallSliding;
    public RaycastHit2D wallHitCheck;
    public float jumpTime;

    // Players Jump Options
    public float wallJumpTime;
    public float wallSlideSpeed;

    [Header("Player Test Values")]
    public bool isJumping;
    public bool isFacingRight;
    public bool isCanJump;

    protected Player player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Player newPlayer = new Player(0, 1, 1.0f, 20.0f, false, true, gameObject.GetComponent<Rigidbody2D>());
        player = newPlayer;

        wallJumpTime = 0.2f;
        wallSlideSpeed = 0.3f;
        wallDistance = 0.18f;
        isWallSliding = false;
        isCanJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();

        // testing elements
        isFacingRight = player.IsFacingRight;
        isJumping = player.IsJumping;

   
    }

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

    private void FlipCheck()
    {
        if (player.MoveHorizontal > 0.1f && player.IsFacingRight == false) Flip();
        else if (player.MoveHorizontal < -0.1f && player.IsFacingRight == true) Flip();
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        player.IsFacingRight = !player.IsFacingRight;
    }

    private void WallSlidingCheck()
    {
        if (player.IsFacingRight)
        {
            wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, GroundLayer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.red);
        }
        else
        {
            wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, GroundLayer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.red);
        }

        // (player.MoveHorizontal > 0.1f || player.MoveHorizontal < -0.1f)
        if (wallHitCheck && player.IsJumping )
        {
            isWallSliding = true;
            isCanJump = true;
            wallJumpTime = Time.time + wallJumpTime;

        }
        else isWallSliding = false;


        if (isWallSliding && player.MoveVertical < -0.1f)
        {
            player.MoveVertical *= 0.1f;
                }
        /*if (jumpTime < Time.time)
        {
            isWallSliding = false;
            wallJumpTime = 0f;
        }*/

        if (isWallSliding)
        {
            player.Rigidbody2D.AddForce(new Vector2(0f, player.MoveVertical * (1.0f)), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject == GroundObject)
        {
            player.IsJumping = false;
            isCanJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GroundObject)
        {
            player.IsJumping = true;
            isCanJump = false;
        }
    }
}

