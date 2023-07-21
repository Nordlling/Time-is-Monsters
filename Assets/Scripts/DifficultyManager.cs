using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public event Action OnIncreaseLevel;
    
    [SerializeField] private int frequency = 10;
    private float _leftTime;
    public int Level { get; private set; }

    private void Start()
    {
        _leftTime = frequency;
    }

    private void Update()
    {
        if (_leftTime < 0)
        {
            _leftTime = frequency;
            Level++;
            OnIncreaseLevel?.Invoke();
        }
        _leftTime -= Time.deltaTime;
    }
}
