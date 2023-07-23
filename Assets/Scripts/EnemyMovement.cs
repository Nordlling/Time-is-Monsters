using System;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [Inject] private DifficultyManager _difficultyManager;
    
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float coef = 1.2f;
    
    private Transform _transform;
    private bool _isChase;
    private float _distance;
    private Vector3 _direction;
    
    private void Start()
    {
        for (int i = 0; i < _difficultyManager.DifficultyLevel; i++)
        {
            speed *= coef;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
                break;
            }
            animator.SetFloat("speedMultiplier", animator.GetFloat("speedMultiplier") * coef);
        }
        
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        _direction = new Vector3(randomDirection.x, 0f, randomDirection.y).normalized;
        transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Vector3 collisionDirection = collision.contacts[0].normal;
            Vector3 objectDirection = transform.forward;
            
            float angle = Vector3.Angle(collisionDirection, -objectDirection);
            float sign = Mathf.Sign(Vector3.Dot(collisionDirection, transform.right));
            angle *= -sign;
            
            float minAngle = 100 + angle;
            float maxAngle = 170 + minAngle;
            
            float resultAngle = UnityEngine.Random.Range(minAngle, maxAngle);
            Vector3 newDirection = Quaternion.AngleAxis(resultAngle, transform.up) * objectDirection;
            transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
        }
    }
}