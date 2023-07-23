using System;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    // [Inject] private GameOverNotifier _gameOverNotifier;
    [Inject] private DifficultyManager _difficultyManager;
    
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxSpeed = 24f;
    [SerializeField] private float difficultyСoef = 1.2f;

    [SerializeField] private bool noRandom;
    
    private Transform _transform;
    private bool _isChase;
    private float _distance;
    private Vector3 _direction;

    // private void OnEnable()
    // {
    //     _difficultyManager.OnComplicate += Complicate;
    // }
    // private void OnDisable()
    // {
    //     _difficultyManager.OnComplicate -= Complicate;
    // }
    
    private void Start()
    {
        for (int i = 0; i < _difficultyManager.Level; i++)
        {
            
            speed *= difficultyСoef;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
                break;
            }
            animator.SetFloat("speedMultiplier", animator.GetFloat("speedMultiplier") * difficultyСoef);
        }
        if (noRandom)
        {
            return;
        }
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        _direction = new Vector3(randomDirection.x, 0f, randomDirection.y).normalized;
        // transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        // Debug.Log(_direction);
        transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);
    }

    private void FixedUpdate()
    {
        Walk();
        // if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10f))
        // {
        //     Vector3 wallNormal = hit.normal;
        //     Debug.DrawRay(transform.position, wallNormal * 30, Color.red);   
        // }
    }

    private void Walk()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Vector3 collisionDirection = collision.contacts[0].normal;
            Vector3 objectDirection = -transform.forward;
            float angle = Vector3.Angle(collisionDirection, objectDirection);
            
            float sign = Mathf.Sign(Vector3.Dot(collisionDirection, transform.right));
            angle *= sign;
            float minAngle = 100 + -angle;
            float maxAngle = 170 + minAngle;
            float resultAngle = UnityEngine.Random.Range(minAngle, maxAngle);
            // Debug.Log($"angle = {angle}; minAngle = {minAngle}; maxAngle = {maxAngle}; resultAngle = {resultAngle}");
            Vector3 newDirection = Quaternion.AngleAxis(resultAngle, transform.up) * -objectDirection;
            transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
            
            // Debug.Log("Angle: " + angle);

            //     if (Physics.Raycast(collision.transform.position, transform.forward, out RaycastHit hit, 1f))
            //     {
            //         Debug.Log("Нормаль стены: " + hit.normal);
            //     }
            //     collision.transform
        }
    }

    private void Complicate()
    {
        speed *= difficultyСoef;
    }
}