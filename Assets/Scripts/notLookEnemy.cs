using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notLookEnemy : Item
{
    public override void Buff()
    {
        base.Buff();
        GameManager.Instance.NotLookEnemy = true;
    }
}
