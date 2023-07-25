using System;
using TMPro;
using UnityEngine;
using Zenject;

public class ButtonTimer : MonoBehaviour
{
    [Inject] private Boosters boosters;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject image;
    [SerializeField] private BoosterTypeEnum boosterType;

    private void OnEnable()
    {
        boosters.OnUpdateButtonTimer += UpdateButtonTimer;
    }

    private void OnDisable()
    {
        boosters.OnUpdateButtonTimer -= UpdateButtonTimer;
    }

    private void UpdateButtonTimer(BoosterTypeEnum boosterType, float time)
    {
        if (this.boosterType.Equals(boosterType))
        {
            time = (float)Math.Round(time, 1);
            image.SetActive(true);
            timerText.text = time.ToString("F1");
            
            if (time == 0)
            {
                image.SetActive(false);
            }
        }
    }
}
