using System;
using UnityEngine;
using Zenject;

public class PlayerShooting : MonoBehaviour
{
    [Inject] private DiContainer diContainer;
    [Inject] private Boosters boosters;
    
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform point;
    [SerializeField] private float ballSpeed = 3000f;
    [SerializeField] private float recharge = 0.5f;
    [SerializeField] private float coef = 0.9f;

    private int layerMask;
    private float _leftTime;
    private Vector3 worldMousePosition;


    private void OnEnable()
    {
        boosters.OnUpgradeRateFire += UpgradeRateFire;
    }
    private void OnDisable()
    {
        boosters.OnUpgradeRateFire -= UpgradeRateFire;
    }

    private void Start()
    {
        _leftTime = 0;
        layerMask = ~LayerMask.GetMask("Border");
    }

    private void Update()
    {
        if (_leftTime > 0)
        {
            _leftTime -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && _leftTime <= 0)
        {
            animator.SetTrigger("shoot");
            Attack();
            _leftTime = recharge;
        }
    }
    
    public void Attack()
    {
        Vector3 mousePosition = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            worldMousePosition = hit.point;
        }
        Vector3 direction = worldMousePosition - point.position;
        direction.Normalize();

        var fireBall = diContainer.InstantiatePrefab(ballPrefab, point.position, Quaternion.identity, null);
        fireBall.GetComponent<Rigidbody>().AddForce(direction * ballSpeed);
    }
    
    private void UpgradeRateFire()
    {
        if (recharge > 0.0001f)
        {
            recharge *= coef;
        }
    }
}
