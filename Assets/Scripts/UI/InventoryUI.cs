using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine;

public class InventoryUI : BaseUI
{
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private TextMeshProUGUI CountText;
    private List<GameObject> inventorySlots = new List<GameObject>();
    private void Awake()
    {
        Hide();
        
    }

    private void Update()
    {
        CountText.text = ItemManager.Instance.Inventoryitmes.Count + "/" + ItemManager.Instance.MaxInventory;
    }

    public void InventoryItemUpdate()
    {
        foreach (var slot in inventorySlots)
        {
            Destroy(slot);
        }
        inventorySlots.Clear();
        
        foreach (Item item in ItemManager.Instance.Inventoryitmes)
        {
            var slot = Instantiate(inventorySlotPrefab);
            slot.transform.SetParent(transform.GetChild(0).GetChild(0));
            InventorySlot inventorySlot =  slot.GetComponent<InventorySlot>();
            inventorySlot.itemName = item.itemName;
            inventorySlot.itemSprite = item.itemSprite;
            inventorySlot.itemDescription = item.itemDescription;
            inventorySlot.itemPrice = item.itemPrice;
            inventorySlot.itemCount = item.itemCount;
            inventorySlot.Color = item.Color;
            inventorySlots.Add(slot);
        }
    }
}
