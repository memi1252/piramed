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
    [SerializeField] private Button BagbuyBtn;
    [SerializeField] private Text BagText;
    [SerializeField] private int BagPrice = 1000;
    
    
    private int oxygenBagIndex = 0;
    private int BagIndex = 0;
    

    [Serializable]
    public struct oxygenBag
    {
        public string name;
        public int price;
        public int oxygen;
    }
    
    [Serializable]
    public struct  bag
    {
        public string name;
        public int price;
        public int count;
        public float weight;
    }
    
    [SerializeField] private oxygenBag[] oxygenBags = new oxygenBag[3];
    [SerializeField] private bag[] bags = new bag[3];
    
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
                StartCoroutine(Success(oxygenBagbuyBtn));
                oxygenBagIndex++;
            }
            else
            {
                StartCoroutine(Failed(oxygenBagbuyBtn));
            } 
        });
        BagbuyBtn.onClick.AddListener(() =>
        {
            if (GameManager.Instance.price >= BagPrice)
            {
                GameManager.Instance.price -= BagPrice;
                ItemManager.Instance.MaxInventory = bags[BagIndex].count;
                //GameManager.Instance.Player.maxWeight = bags[BagIndex].weight;
                StartCoroutine(Success(BagbuyBtn));
                BagIndex++;
            }
            else
            {
                StartCoroutine(Failed(BagbuyBtn));
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
        
        if (BagIndex != 2)
        {
            BagText.text = bags[BagIndex].name + " : " + bags[BagIndex].price;
            BagPrice = bags[BagIndex].price;
        }
        else
        {
            BagText.text = "재고 없음";
            BagbuyBtn.interactable = false;
        }
    }
    
    IEnumerator Success(Button button)
    {
        button.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(0.2f);
        button.GetComponent<Image>().color = Color.white;
    }
    
    IEnumerator Failed(Button button)
    {
        button.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        button.GetComponent<Image>().color = Color.white;
    }
}
