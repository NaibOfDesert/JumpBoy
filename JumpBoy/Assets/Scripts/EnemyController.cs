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
    AudioSource audioSource; 

    private void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(enemyDelay != 0)
        {
            if (!playerController.IsDead())
            {
                rigidbody2d.velocity = new Vector2(0, Mathf.Sqrt(playerController.GetPosition()));
                audioSource.volume = transform.position.y/(playerController.GetPosition()*5);  
            }
        }
    }
}
