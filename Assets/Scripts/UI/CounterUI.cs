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
        spawner.OnUpdateDead += UpdateDeadUI;
        spawner.OnUpdateAlive += UpdateAliveUI;
    }
    
    private void OnDisable()
    {
        spawner.OnUpdateDead -= UpdateDeadUI;
        spawner.OnUpdateAlive -= UpdateAliveUI;
    }

    private void UpdateDeadUI(int killed)
    {
        killedCount.text = killed.ToString();
    }
    
    private void UpdateAliveUI(int onField)
    {
        onFieldCount.text = onField.ToString();
    }
}
