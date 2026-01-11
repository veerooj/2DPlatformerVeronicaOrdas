using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    private Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;

    public int damage = 1;

    public int enemyMaxHealth = 3;
    private int enemyCurrentHealth;
    private SpriteRenderer spriteRenderer;
    private Color ogColor;
    
    //LOOT TABLE
    public List<LootItem> lootTable = new List<LootItem>();

    void Start()
    {
        
        
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCurrentHealth = enemyMaxHealth;
        ogColor = spriteRenderer.color;
    }

    void Update()
    {
        //  GROUND CHECK
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        
        //FOLLOWING THE PLAYER
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        
        //DETECTING THE PLAYER
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << player.gameObject.layer);

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocityY);

            RaycastHit2D groundInFront =
                Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);

            RaycastHit2D gapInFront = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0),
                Vector2.down, 2f, groundLayer);

            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, groundLayer);

            if (!groundInFront.collider && !gapInFront.collider)
            {
                shouldJump = true;
            }

            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;
            
            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
            
        }
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        StartCoroutine(FlashDamage());
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashDamage()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = ogColor;
    }

    void Die()
    {
        foreach (LootItem lootItem in lootTable )
        {
            if (UnityEngine.Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefabs);
            }

            break;
        }
        Destroy(gameObject);
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);

            droppedLoot.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
    
}
