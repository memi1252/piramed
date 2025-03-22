using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] public InventoryUI inventoryUI;
    [SerializeField] public StatusUI statusUI;
    [SerializeField] public GameMenuUI gameMenuUI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && GameManager.Instance.GameStart)
        {
            if (inventoryUI.gameObject.activeSelf)
            {
                inventoryUI.Hide();
            }
            else
            {
                inventoryUI.Show();
                inventoryUI.GetComponent<InventoryUI>().InventoryItemUpdate();
            }
        }
    }
}
