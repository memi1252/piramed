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
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit(); 
            #endif
        });
        ShopButton.onClick.AddListener(() =>
        {
            UIManager.Instance.shopUI.Show();
        });
    }

    private void Update()
    {
        GoldText.text = ($"골드 : {GameManager.Instance.price}");
    }
}
