using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button oxygenBagbuyBtn;
    [SerializeField] private Text oxygenBagText;
    [SerializeField] private int oxygenBagPrice = 1000;
    
    
    private int oxygenBagIndex = 0;
    

    [Serializable]
    public struct oxygenBag
    {
        public string name;
        public int price;
        public int oxygen;
    }
    
    [SerializeField] private oxygenBag[] oxygenBags = new oxygenBag[3];
    
    private void Awake()
    {
        closeBtn.onClick.AddListener(() =>
        {
            Hide();
        });
        oxygenBagbuyBtn.onClick.AddListener(() =>
        {
            if (GameManager.Instance.price >= oxygenBagPrice)
            {
                GameManager.Instance.price -= oxygenBagPrice;
                GameManager.Instance.maxPlayerOxygen = oxygenBags[oxygenBagIndex].oxygen;
                oxygenBagIndex++;
            }
        });
        Hide();
    }

    private void Update()
    {
        if (oxygenBagIndex != 3)
        {
            oxygenBagText.text = oxygenBags[oxygenBagIndex].name + " : " + oxygenBags[oxygenBagIndex].price;
            oxygenBagPrice = oxygenBags[oxygenBagIndex].price;
        }
        else
        {
            oxygenBagText.text = "재고 없음";
            oxygenBagbuyBtn.interactable = false;
        }
    }
}
