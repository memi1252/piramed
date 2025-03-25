using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : Item
{
    [SerializeField] private float SpeedUp = 1.5f;
    [SerializeField] private float SpeedUpTime = 5f;

    public override void Buff()
    {
        base.Buff();
        if (GameManager.Instance.SpeedUpOn)
        {
            GameManager.Instance.Player.Movespeed -= GameManager.Instance.SpeedUpSpeed;
            
        }
        GameManager.Instance.SpeedUp(SpeedUp, SpeedUpTime);
    }
}
