using System.Collections;
using System.Collections.Generic;
using UnityEditor.AppleTV;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class enemyController : MonoBehaviour
{
    private PlayerController thePlayer;
    private float moveSpeed;
    private Vector2 moveDirection;

    void Start()
    {
        moveSpeed = 3f;

        if (transform.position.x > 0)
        {
            moveDirection = Vector2.left; // Move left
        }
        else
        {
            moveDirection = Vector2.right; // Move right
        }
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    //modified code from class
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>(); //get player controller
            player.healthPoints -= 1;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EnemyCharacter") || collision.gameObject.CompareTag("Platform"))
        {
            //this ignores enemies and platforms for collision
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Bullet"))
         {
            ScoreManager.instance.AddPoints(100);
            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject); //Destroy the bullet
            }
            Destroy(gameObject); //Destroy the enemy
         }

    }
}
