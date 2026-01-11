using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float bounceForce = 10f;

    public int damage = 1;
    public bool isPlayerAbove;


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            HandlePlayerBounce(other.gameObject);
        }
    }

    private void HandlePlayerBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 0f);
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}