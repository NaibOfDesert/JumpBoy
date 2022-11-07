using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] bool wallHitCheck;
    [SerializeField] float wallDistance;

    [Header("Move")]
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] bool isFaceRight = true;

    [Header("Masks")]
    [SerializeField] LayerMask groundLayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FlipCheck();

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

    private void FlipCheck()
    {
        if ((!isFaceRight && transform.localScale.x > Mathf.Epsilon) || (isFaceRight && transform.localScale.x < -Mathf.Epsilon)) Flip();
    }
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
