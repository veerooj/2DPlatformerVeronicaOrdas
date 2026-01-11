using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 1;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        
    }
}
