using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Zenject;

public class Boosters : MonoBehaviour
{
    [Inject] private Spawner spawner;

    [SerializeField] private float freezeSpawnRecharge = 60f;
    [SerializeField] private float killAllRecharge = 60f;
    [SerializeField] private float upgradeDamageRecharge = 60f;
    [SerializeField] private float upgradeRateFireRecharge = 60f;
    
    private Dictionary<BoosterTypeEnum, TimerInfo> timers = new Dictionary<BoosterTypeEnum, TimerInfo>();

    public event Action OnKillAll;
    public event Action OnUpgradeDamage;
    public event Action OnUpgradeRateFire;
    public event Action<BoosterTypeEnum, float> OnUpdateButtonTimer;

    public int UpgradeLevel { get; private set; }

    private void Start()
    {
        AddTimer(BoosterTypeEnum.FREEZE_SPAWN, freezeSpawnRecharge);
        AddTimer(BoosterTypeEnum.KILL_ALL, killAllRecharge);
        AddTimer(BoosterTypeEnum.UPGRADE_DAMAGE, upgradeDamageRecharge);
        AddTimer(BoosterTypeEnum.UPGRADE_RATE_FIRE, upgradeRateFireRecharge);
    }

    private void Update()
    {
        foreach (KeyValuePair<BoosterTypeEnum, TimerInfo> kvp  in timers)
        {
            BoosterTypeEnum key = kvp.Key;
            TimerInfo timer = kvp.Value;
            if (timer.Started)
            {
                timer.UpdateTimer(Time.deltaTime);
                if (!timer.CooldownDuration.Equals(timer.CooldownTimer))
                {
                    OnUpdateButtonTimer?.Invoke(key, timer.CooldownTimer);
                }
            }
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            ActivateFreezeSpawn();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            ActivateKillAll();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpgradeDamage();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpgradeRateFire();
        }
        
    }
    
    public void ActivateKillAll()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.KILL_ALL;
        if (!timers[boosterType].Started)
        {
            timers[boosterType].UpdateTimer(Time.deltaTime);
            OnKillAll?.Invoke();
        }
    }

    public void ActivateFreezeSpawn()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.FREEZE_SPAWN;
        if (!timers[boosterType].Started)
        {
            spawner.FreezeSpawn();
            timers[boosterType].UpdateTimer(Time.deltaTime);
        }
    }

    public void UpgradeDamage()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.UPGRADE_DAMAGE;
        if (!timers[boosterType].Started)
        {
            UpgradeLevel++;
            timers[boosterType].UpdateTimer(Time.deltaTime);
        }
    }
    public void UpgradeRateFire()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.UPGRADE_RATE_FIRE;
        if (!timers[boosterType].Started)
        {
            OnUpgradeRateFire?.Invoke();
            timers[boosterType].UpdateTimer(Time.deltaTime);
        }
    }
    
    private void AddTimer(BoosterTypeEnum boosterType, float cooldownDuration)
    {
        if (!timers.ContainsKey(boosterType))
        {
            timers.Add(boosterType, new TimerInfo(cooldownDuration));
        }
    }
}