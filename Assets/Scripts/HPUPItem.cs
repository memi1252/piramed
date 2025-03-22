using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUPItem : Item
{
    [SerializeField] public int HpUp = 5;

    public override void Buff()
    {
        base.Buff();
        GameManager.Instance.PlayerHealth += HpUp;
    }
}
