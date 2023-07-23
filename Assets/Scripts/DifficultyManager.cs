using System;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public event Action OnIncreaseLevel;
    
    [SerializeField] private int frequency = 10;
    private float _leftTime;
    public int DifficultyLevel { get; private set; }

    private void Start()
    {
        _leftTime = frequency;
    }

    private void Update()
    {
        if (_leftTime < 0)
        {
            _leftTime = frequency;
            DifficultyLevel++;
            OnIncreaseLevel?.Invoke();
        }
        _leftTime -= Time.deltaTime;
    }
}
