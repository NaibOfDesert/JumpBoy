using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player PlayerOne { get; private set; }
    public Player PlayerTwo { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Player Player = new Player(0, 1, 1.2f, 20.0f, gameObject.GetComponent<Rigidbody2D>());
        PlayerOne = Player;
        Debug.Log(PlayerOne.JumpForce);
        PlayerOne.IsJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOne.MoveHorizontal = Input.GetAxisRaw("Horizontal"); // (-1, 0, 1)
        PlayerOne.MoveVertical = Input.GetAxisRaw("Vertical"); // (-1, 0, 1)
    }

    // Update is called for physics changes
    void FixedUpdate()
    {
        if(PlayerOne.MoveHorizontal > 0.1f || PlayerOne.MoveHorizontal < -0.1f)
        {
            PlayerOne.Rigidbody2D.AddForce(new Vector2(PlayerOne.MoveHorizontal * PlayerOne.MoveSpeed, 0f), ForceMode2D.Impulse);
        }
        
        if (!PlayerOne.IsJumping && PlayerOne.MoveVertical > 0.1f)
        {
            PlayerOne.Rigidbody2D.AddForce(new Vector2(0f, PlayerOne.MoveVertical * PlayerOne.JumpForce), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            PlayerOne.IsJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            PlayerOne.IsJumping = true;
        }
    }
}

