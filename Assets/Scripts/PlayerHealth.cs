using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;

    private int currentHealth;

    public HealthUI healthUI;
    private SpriteRenderer spriteRenderer;

    

    public static event Action OnPlayerDied;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetHealth();

        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.OnReset += ResetHealth;
        HealthItem.OnHealthCollect += Heal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetHealth()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }

        Trap trap = other.GetComponent<Trap>();
        if (trap && trap.damage > 0)
        {
            TakeDamage(trap.damage);
        }
    }

    private void TakeDamage(int damage)
    {
       
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        StartCoroutine(FlashDamage());
        if (currentHealth <= 0)
        {
            
            OnPlayerDied?.Invoke();
            
        }
        
    }

    private IEnumerator FlashDamage()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    void OnDestroy()
    {
        GameManager.OnReset -= ResetHealth;
    }

    void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            
        }
        healthUI.UpdateHearts(currentHealth);
    }
}
