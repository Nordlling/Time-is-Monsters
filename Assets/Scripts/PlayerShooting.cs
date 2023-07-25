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
    [SerializeField] private int doubleShootsCount = 3;

    [SerializeField] private AudioSource shootAudio;
    [SerializeField] private ParticleSystem  shootEffect;

    private int _layerMask;
    private float _leftTime;
    private Vector3 _worldMousePosition;
    private int _availableDoubleShoots;


    private void OnEnable()
    {
        boosters.OnUpgradeRateFire += UpgradeRateFire;
        boosters.OnUpgradeDoubleShoot += UpgradeDoubleShoot;
    }
    private void OnDisable()
    {
        boosters.OnUpgradeRateFire -= UpgradeRateFire;
        boosters.OnUpgradeDoubleShoot -= UpgradeDoubleShoot;
    }

    private void Start()
    {
        _leftTime = 0;
        _layerMask = ~LayerMask.GetMask("Border");
    }

    private void Update()
    {
        if (_leftTime > 0)
        {
            _leftTime -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && _leftTime <= 0)
        {
            Shoot();
        }
    }
    
    public void Shoot()
    {
        shootEffect.Play();
        shootAudio.Play();
        animator.SetTrigger("shoot");
        Vector3 mousePosition = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            _worldMousePosition = hit.point;
        }
        Vector3 direction = _worldMousePosition - point.position;
        direction.Normalize();
        
        spawnBall(direction);
        if (_availableDoubleShoots > 0)
        {
            spawnBall(direction);
            _availableDoubleShoots--;
        }
        
        _leftTime = recharge;
    }

    private void spawnBall(Vector3 direction)
    {
        var ball = diContainer.InstantiatePrefab(ballPrefab, point.position, Quaternion.identity, null);
        ball.GetComponent<Rigidbody>().AddForce(direction * ballSpeed);
    }
    
    private void UpgradeRateFire()
    {
        if (recharge > 0.0001f)
        {
            recharge *= coef;
        }
    }
    private void UpgradeDoubleShoot()
    {
        _availableDoubleShoots = doubleShootsCount;
    }
}
