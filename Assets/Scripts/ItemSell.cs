using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSell : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemManager itemManager = ItemManager.Instance;
            foreach (Item item in itemManager.Inventoryitmes)
            {
                GameManager.Instance.price += item.itemPrice;
            }
            itemManager.Inventoryitmes.Clear();
            foreach (var box in GameManager.Instance.NowOpenBoxs)
            {
                if (box.GetComponent<Box>().buffItem)
                {
                    box.isOpen = false;
                    box.GetComponent<Animator>().SetBool("BoxOpen", false);
                }
            }
            itemManager.NowGetItemList.Clear();
            GameManager.Instance.NowOpenBoxs.Clear();
            GameManager.Instance.ISDungenExit();
        }
    }
}
