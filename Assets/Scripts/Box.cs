using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    [SerializeField] public bool isOpen = false;
    [SerializeField] public bool buffItem = false;

    
    private void Start()
    {
        GameManager.Instance.Boxs.Add(this);
    }

    public void OpenBox()
    {
        if (!isOpen)
        {
            ItemManager itemManager = ItemManager.Instance;
            if (!buffItem)
            {
                int RandomItemID = Random.Range(0, itemManager.ItemList.Count);
                Item item = itemManager.ItemList[RandomItemID];
                itemManager.Inventoryitmes.Add(item);
                itemManager.NowGetItemList.Add(item);
                itemManager.ItemList.RemoveAt(RandomItemID);
                UIManager.Instance.inventoryUI.GetComponent<InventoryUI>().InventoryItemUpdate();
                Debug.Log($"아이템이름{item.itemName}아이템가격{item.itemPrice}아이템설명{item.itemCount}");
            }
            else
            {
                int RandomItemID = Random.Range(0, itemManager.BuffItemList.Count);
                itemManager.Inventoryitmes.Add(itemManager.BuffItemList[RandomItemID]);
                Item item = itemManager.BuffItemList[RandomItemID];
                item.Buff();
                UIManager.Instance.inventoryUI.GetComponent<InventoryUI>().InventoryItemUpdate();
                Debug.Log($"아이템이름{item.itemName}아이템가격{item.itemPrice}아이템설명{item.itemCount}");
            }
           
            
            isOpen = true;
        }
    }
}
