using UnityEngine;
using Zenject;

public class Backlight : MonoBehaviour
{
    [Inject] private Spawner spawner;

    [SerializeField] private float timeToShow = 2f;
    [SerializeField] private GameObject leftBacklight;
    [SerializeField] private GameObject rightBacklight;
    
    private void OnEnable()
    {
        spawner.OnActivateLeftBacklight += ActivateLeftBacklight;
        spawner.OnActivateRightBacklight += ActivateRightBacklight;
    }
    
    private void OnDisable()
    {
        spawner.OnActivateLeftBacklight -= ActivateLeftBacklight;
        spawner.OnActivateRightBacklight -= ActivateRightBacklight;
    }

    private void ActivateLeftBacklight()
    {
        CancelInvoke(nameof(DeactivateLeftBacklight));
        leftBacklight.SetActive(true);
        Invoke(nameof(DeactivateLeftBacklight), timeToShow);
    }
    
    private void ActivateRightBacklight()
    {
        CancelInvoke(nameof(DeactivateRightBacklight));
        rightBacklight.SetActive(true);
        Invoke(nameof(DeactivateRightBacklight), timeToShow);

    }
    
    private void DeactivateLeftBacklight()
    {
        leftBacklight.SetActive(false);
    }
    
    private void DeactivateRightBacklight()
    {
        rightBacklight.SetActive(false);
    }
}
