using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : BaseUI
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI itemCountText;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }


    public void SetItem(string itemName, string itemDescription, int itemPrice, int itemCount)
    {
        itemNameText.text = ($"name : {itemName}");
        itemDescriptionText.text = ($"description : {itemDescription}");
        itemPriceText.text = ($"price : {itemPrice.ToString()}");
        itemCountText.text = ($"count : {itemCount.ToString()}");
    }
    
    
    
    
}
