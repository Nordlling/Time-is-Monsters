﻿using UnityEngine;
using Zenject;

public class BallAttack : MonoBehaviour
{
    [Inject] private Boosters boosters;
    
    [SerializeField] private float damage = 40f;
    [SerializeField] private float coef = 1.2f;
    
    [SerializeField] private GameObject damageEffect;

    private void Start()
    {
        for (int i = 0; i < boosters.UpgradeLevel; i++)
        {
            damage *= coef;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
