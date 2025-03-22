using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //아이템 이름
    [SerializeField] public string itemName;
    //아이템 스프리트
    [SerializeField] public Sprite itemSprite;
    //아이템 설명
    [SerializeField] public string itemDescription;
    //아이템 가격
    [SerializeField] public int itemPrice;
    //아이템 갯수
    [SerializeField] public int itemCount;
    [SerializeField] public Color Color = Color.white;



    public virtual void Buff()
    {
        
    }
}
