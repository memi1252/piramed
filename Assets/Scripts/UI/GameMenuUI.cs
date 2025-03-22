using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameMenuUI : BaseUI
{
    [SerializeField] public Button GameStartButton;
    [SerializeField] public Button ExitButton;
    [SerializeField] public Button HelpButton;
    [SerializeField] public Button ShopButton;

    [SerializeField]
    public Text GoldText;

    private void Awake()
    {
        GameStartButton.onClick.AddListener(() => { GameManager.Instance.InGameStart(); });
        ExitButton.onClick.AddListener(() =>
        {
           Application.Quit(); 
        });
    }

    private void Update()
    {
        GoldText.text = ($"Gold : {GameManager.Instance.price}");
    }
}
