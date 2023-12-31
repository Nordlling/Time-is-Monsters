using System;
using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour
{
    [Inject] private DifficultyManager difficultyManager;
    [Inject] private Boosters boosters;
    [Inject] private Spawner spawner;

    [SerializeField] private Animator animator;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float coef = 1.2f;

    private float _currentHealth;
    private EnemyMovement _enemyMovement;
    
    [SerializeField] private AudioSource damageEffect;
    [SerializeField] private AudioSource dieAudio;
    
    [SerializeField] private ParticleSystem dieEffect;
    
    private void OnEnable()
    {
        boosters.OnKillAll += Die;
    }
    private void OnDisable()
    {
        boosters.OnKillAll -= Die;
    }

    private void Start()
    {
        for (int i = 0; i < difficultyManager.DifficultyLevel; i++)
        {
            maxHealth *= coef;
        }
        _currentHealth = maxHealth;
        _enemyMovement = GetComponent<EnemyMovement>();
        damageEffect.enabled = true;
    }
    
    public void TakeDamage(float damage)
    {
            if (damage < 0)
            {
                throw new Exception("Negative damage");
            }

            damageEffect.Play();
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
    }

    private void Die()
    {
        dieAudio.Play();
        dieEffect.Play();
        if (!_enemyMovement.enabled)
        {
            return;
        }
        
        spawner.KillEnemy();
        _enemyMovement.enabled = false;
        animator.SetTrigger("die");
        Invoke(nameof(Disappear), 3f);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
