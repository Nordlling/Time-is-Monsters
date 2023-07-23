using TMPro;
using UnityEngine;
using Zenject;

public class CounterUI : MonoBehaviour
{
    [Inject] private Spawner spawner;
    [SerializeField] private TextMeshProUGUI killedCount;
    [SerializeField] private TextMeshProUGUI onFieldCount;

    private void OnEnable()
    {
        spawner.OnUpdateKilled += UpdateKilledUI;
        spawner.OnUpdateOnField += UpdateOnFieldUI;
    }
    
    private void OnDisable()
    {
        spawner.OnUpdateKilled -= UpdateKilledUI;
        spawner.OnUpdateOnField -= UpdateOnFieldUI;
    }

    private void UpdateKilledUI(int killed)
    {
        killedCount.text = killed.ToString();
    }
    
    private void UpdateOnFieldUI(int onField)
    {
        onFieldCount.text = onField.ToString();
    }
}
