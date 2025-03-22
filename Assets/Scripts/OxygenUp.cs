using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenUp : Item
{
    [SerializeField] public int oxygenUp = 5;

    public override void Buff()
    {
        base.Buff();
        GameManager.Instance.PlayerOxygen += oxygenUp;
    }
}
