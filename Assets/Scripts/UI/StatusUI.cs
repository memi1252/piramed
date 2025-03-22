using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : BaseUI
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private Text GoldText;


    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        StatusSlider();
        GoldUpdate();
    }

    private void StatusSlider(){
        hpSlider.value = GameManager.Instance.PlayerHealth/GameManager.Instance.maxPlayerHealth;
        oxygenSlider.value = GameManager.Instance.PlayerOxygen/GameManager.Instance.maxPlayerOxygen;
    }

    private void GoldUpdate()
    {
        GoldText.text = ($"Gold : {GameManager.Instance.price}");
    }
}
