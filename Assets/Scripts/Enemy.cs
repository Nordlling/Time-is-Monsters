using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private int maxHealth = 100;
    
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(40);
            Destroy(collision.gameObject);
        }
    }
    
    private void TakeDamage(int damage)
    {
            if (damage < 0)
            {
                throw new Exception("Negative damage");
            }

            _currentHealth -= damage;
            // OnUpdate?.Invoke(_currentHealth, maxHealth);

            if (_currentHealth <= 0)
            {
                Die();
            }
    }

    private void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);
        // todo: animation
    }
}
