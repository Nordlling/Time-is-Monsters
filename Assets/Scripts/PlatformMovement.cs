using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    
    [SerializeField] private float speed = 2f;
    [SerializeField] private Collider platformCollider;
    
    private float minX;
    private float maxX;

    private Vector3 _direction;

    private void Start()
    {
        var platformBounds = platformCollider.bounds;
        minX = platformBounds.min.x;
        maxX = platformBounds.max.x;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if ((transform.position.x <= minX && horizontalInput < 0) ||
            (transform.position.x >= maxX && horizontalInput > 0))
        {
            return;
        }

        _direction.x = horizontalInput * speed * Time.deltaTime;
        transform.position += _direction;
    }
}
