using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Inject] private DifficultyManager difficultyManager;
    [Inject] private Boosters boosters;
    [Inject] private Spawner spawner;

    [SerializeField] private Animator animator;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float difficultyСoef = 1.2f;

    private float _currentHealth;
    private EnemyMovement _enemyMovement;

    // [Inject] 
    // public void Construct(DifficultyManager difficultyManager)
    // {
    //     _difficultyManager = difficultyManager;
    //     _difficultyManager.OnComplicate += Complicate;
    // }
    
    private void OnEnable()
    {
        difficultyManager.OnIncreaseLevel += Complicate;
        boosters.OnKillAll += Die;
    }
    private void OnDisable()
    {
        difficultyManager.OnIncreaseLevel -= Complicate;
        boosters.OnKillAll -= Die;
    }

    private void Start()
    {
        for (int i = 0; i < difficultyManager.Level; i++)
        {
            maxHealth *= difficultyСoef;
        }
        _currentHealth = maxHealth;
        _enemyMovement = GetComponent<EnemyMovement>();
    }
    
    public void TakeDamage(float damage)
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
        if (!_enemyMovement.enabled)
        {
            return;
        }
        
        // Destroy(gameObject);
        spawner.KillEnemy();
        // Debug.Log("Die");
        _enemyMovement.enabled = false;
        animator.SetTrigger("die");
        Invoke(nameof(Disappear), 3f);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }

    private void Complicate()
    {
    }
}
