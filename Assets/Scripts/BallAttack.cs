using System;
using UnityEngine;
using Zenject;

public class BallAttack : MonoBehaviour
{
    [Inject] private Boosters boosters;
    
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float damage = 40f;
    [SerializeField] private float coeff = 1.2f;
    

    private void OnEnable()
    {
        boosters.OnUpgradeDamage += UpgradeDamage;
    }

    private void OnDisable()
    {
        boosters.OnUpgradeDamage -= UpgradeDamage;
    }

    private void Start()
    {
        for (int i = 0; i < boosters.UpgradeLevel; i++)
        {
            damage *= coeff;
        }
    }

    private void UpgradeDamage()
    {
        damage *= coeff;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            Enemy enemyHealth = other.gameObject.GetComponent<Enemy>();
            enemyHealth.TakeDamage(damage);
        }
        // Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        // Destroy(gameObject);
    }
}
