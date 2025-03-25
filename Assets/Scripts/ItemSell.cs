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
                if (GameManager.Instance.Coin2X)
                {
                    GameManager.Instance.price += item.itemPrice*2;
                    GameManager.Instance.Coin2X = false;
                }
                else
                {
                    GameManager.Instance.price += item.itemPrice;
                }
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
            GameManager.Instance.ISDungenExit(new Vector3(-88, 14, 0));
        }
    }
}
