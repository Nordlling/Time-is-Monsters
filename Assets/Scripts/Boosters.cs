using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Boosters : MonoBehaviour
{
    [Inject] private Spawner spawner;

    [SerializeField] private float killAllRecharge = 60f;
    [SerializeField] private float freezeSpawnRecharge = 20f;
    [SerializeField] private float doubleShootRecharge = 30f;
    [SerializeField] private float upgradeDamageRecharge = 15f;
    [SerializeField] private float upgradeRateFireRecharge = 20f;
    
    private Dictionary<BoosterTypeEnum, TimerInfo> _timers = new Dictionary<BoosterTypeEnum, TimerInfo>();

    public event Action OnKillAll;
    public event Action OnUpgradeRateFire;
    public event Action OnUpgradeDoubleShoot;
    public event Action<BoosterTypeEnum, float> OnUpdateButtonTimer;

    public int UpgradeLevel { get; private set; }

    private void Start()
    {
        AddTimer(BoosterTypeEnum.KILL_ALL, killAllRecharge);
        AddTimer(BoosterTypeEnum.FREEZE_SPAWN, freezeSpawnRecharge);
        AddTimer(BoosterTypeEnum.DOUBLE_SHOOT, doubleShootRecharge);
        AddTimer(BoosterTypeEnum.UPGRADE_DAMAGE, upgradeDamageRecharge);
        AddTimer(BoosterTypeEnum.UPGRADE_RATE_FIRE, upgradeRateFireRecharge);
    }

    private void Update()
    {
        foreach (KeyValuePair<BoosterTypeEnum, TimerInfo> kvp  in _timers)
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
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateKillAll();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ActivateFreezeSpawn();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateDoubleShoot();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UpgradeDamage();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UpgradeRateFire();
        }
        
    }
    
    public void ActivateKillAll()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.KILL_ALL;
        if (!_timers[boosterType].Started)
        {
            _timers[boosterType].UpdateTimer(Time.deltaTime);
            OnKillAll?.Invoke();
        }
    }

    public void ActivateFreezeSpawn()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.FREEZE_SPAWN;
        if (!_timers[boosterType].Started)
        {
            spawner.FreezeSpawn();
            _timers[boosterType].UpdateTimer(Time.deltaTime);
        }
    }

    public void ActivateDoubleShoot()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.DOUBLE_SHOOT;
        if (!_timers[boosterType].Started)
        {
            OnUpgradeDoubleShoot?.Invoke();
            _timers[boosterType].UpdateTimer(Time.deltaTime);
        }
    }


    public void UpgradeDamage()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.UPGRADE_DAMAGE;
        if (!_timers[boosterType].Started)
        {
            UpgradeLevel++;
            _timers[boosterType].UpdateTimer(Time.deltaTime);
        }
    }
    public void UpgradeRateFire()
    {
        const BoosterTypeEnum boosterType = BoosterTypeEnum.UPGRADE_RATE_FIRE;
        if (!_timers[boosterType].Started)
        {
            OnUpgradeRateFire?.Invoke();
            _timers[boosterType].UpdateTimer(Time.deltaTime);
        }
    }
    
    private void AddTimer(BoosterTypeEnum boosterType, float cooldownDuration)
    {
        if (!_timers.ContainsKey(boosterType))
        {
            _timers.Add(boosterType, new TimerInfo(cooldownDuration));
        }
    }
}