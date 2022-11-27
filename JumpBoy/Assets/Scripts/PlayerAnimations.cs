using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    PlayerController playerController; 
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void AnimationSwitch(string value)
    {
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
