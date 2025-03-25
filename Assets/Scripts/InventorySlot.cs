using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public int itemPrice;
    [SerializeField] public int itemCount;
    [SerializeField] private GameObject tooltip;
    [SerializeField] public Color Color;
 
    private Image image;
    private Text Text;

    private void Awake()
    {
        image = GetComponent<Image>();
        Text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        image.sprite = itemSprite;
        Text.text = itemName;
    }

    private void Update()
    {
        image.color = Color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
        tooltip.GetComponent<Tooltip>().SetItem(itemName, itemDescription, itemPrice, itemCount);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
