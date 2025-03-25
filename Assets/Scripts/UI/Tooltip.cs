using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : BaseUI
{
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemDescriptionText;
    [SerializeField] private Text itemPriceText;
    [SerializeField] private Text itemCountText;

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
        itemNameText.text = ($"이름 : {itemName}");
        itemDescriptionText.text = ($"설명 : {itemDescription}");
        itemPriceText.text = ($"가격 : {itemPrice.ToString()}");
        itemCountText.text = ($"갯수 : {itemCount.ToString()}");
    }
    
    
    
    
}
