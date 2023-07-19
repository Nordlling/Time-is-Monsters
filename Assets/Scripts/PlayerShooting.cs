using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform point;
    [SerializeField] private float speed = 6000f;

    private Vector3 worldMousePosition;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("shoot");
            Attack();
        }
    }
    
    public void Attack()
    {
        Vector3 mousePosition = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            worldMousePosition = hit.point;
        }
        Vector3 direction = worldMousePosition - point.position;
        direction.Normalize();

        var fireBall = Instantiate(ballPrefab, point.position, Quaternion.identity);
        fireBall.GetComponent<Rigidbody>().AddForce(direction * speed);
    }
}
