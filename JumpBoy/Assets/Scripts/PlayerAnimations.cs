using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    PlayerController playerController; 
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationSwitch(string value) // toRebuild
    {
        // Debug.Log(rigidbody2d.velocity.x + ", " + rigidbody2d.velocity.y);
        switch (value)
        {
            case "isMovement":
                {
                    animator.SetBool("isRun", playerController.GetIsMove());
                    animator.SetBool("isJump", (playerController.GetIsJump() && (playerController.GetRigidbody2D().velocity.y > Mathf.Epsilon)));
                    animator.SetBool("isFall", (playerController.GetIsJump() && (playerController.GetRigidbody2D().velocity.y < -Mathf.Epsilon)));

                    break;
                }
            case "isDead":
                {
                    animator.SetTrigger("isDead");
                    break;
                }
            default:
                break;
        }



    }

}
