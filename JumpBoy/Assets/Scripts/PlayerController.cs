using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Wall Jump")]
    public LayerMask GroundLayer;
    public Object GroundObject;

    protected Player player { get; private set; }

    // Players Jump Options
    protected float wallJumpTime { get; set; }
    protected float wallSlideSpeed { get; set; }
    protected float wallDistance { get; set; }
    protected bool isWallSliding { get; set; }
    protected RaycastHit2D wallHitCheck { get; set; }
    protected float jumTime {get; set;}



    // Start is called before the first frame update
    void Start()
    {
        Player newPlayer = new Player(0, 1, 1.0f, 2.0f, false, true, gameObject.GetComponent<Rigidbody2D>());
        player = newPlayer;

        wallJumpTime = 0.2f;
        wallSlideSpeed = 0.3f;
        wallDistance = 0.5f;
        isWallSliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();
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
        
        if (player.MoveHorizontal > 0.1f && player.IsFacingRight == false) Flip();
        else if (player.MoveHorizontal < -0.1f && player.IsFacingRight == true) Flip();

        if (player.IsFacingRight) {
            wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, GroundLayer);
                }
        else wallHitCheck = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, GroundLayer);

        
        if(wallHitCheck && player.IsJumping && (player.MoveHorizontal > 0.1f || player.MoveHorizontal < -0.1f))
        {
            isWallSliding = true;
            wallJumpTime = Time.time + wallJumpTime;

        } else if (jumTime < Time.time) {
            isWallSliding = false; 
        } 

        if (isWallSliding)
        {
            player.Rigidbody2D.AddForce(new Vector2(0f, player.MoveVertical * (-0.5f)), ForceMode2D.Impulse);
        }
        
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
        if (!player.IsJumping && player.MoveVertical > 0.1f)
        {
            player.Rigidbody2D.AddForce(new Vector2(0f, player.MoveVertical * player.JumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GroundObject)
        {
            player.IsJumping = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GroundObject)
        {
            player.IsJumping = true;
        }
    }
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        player.IsFacingRight = !player.IsFacingRight;
    }
}

