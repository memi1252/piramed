using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] public List<Item> Inventoryitmes = new List<Item>();
    [SerializeField] public List<Item> ItemList = new List<Item>();
    [SerializeField] public List<Item> BuffItemList = new List<Item>();
    [SerializeField] public List<Item> NowGetItemList = new List<Item>();

    [SerializeField] public int MaxInventory = 5;
}
