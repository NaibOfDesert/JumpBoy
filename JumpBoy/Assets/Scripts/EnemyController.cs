using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerController playerController;

    [Header("Movement")]
    [SerializeField] int enemyDelay;

    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyDelay != 0)
        {
            if (!playerController.IsDead()) rigidbody2d.velocity = new Vector2(0, playerController.GetPosition() / enemyDelay);
            else rigidbody2d.velocity = new Vector2(0, 0);
        }
    }

}
