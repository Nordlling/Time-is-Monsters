using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float lifecycle = 4f;

    private void Update()
    {
        if (lifecycle < 0)
        {
            Destroy(gameObject);
        }

        lifecycle -= Time.deltaTime;
    }
}
