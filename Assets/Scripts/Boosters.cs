using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Boosters : MonoBehaviour
{
    [Inject] private Spawner spawner;
    
    public event Action OnKillAll;
    public event Action OnUpgradeDamage;
    public event Action OnUpgradeShootSpeed;

    public int UpgradeLevel { get; private set; }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            spawner.FreezeSpawn();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            OnKillAll?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpgradeLevel++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnUpgradeShootSpeed?.Invoke();
        }
        
    }
}
