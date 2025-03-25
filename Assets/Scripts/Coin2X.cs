using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2X : Item
{
    public override void Buff()
    {
        base.Buff();
        GameManager.Instance.Coin2X= true;
    }
}
