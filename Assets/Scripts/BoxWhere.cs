using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWhere : Item
{
    public override void Buff()
    {
        base.Buff();
        
        GameManager.Instance.boxWhere = true;
    }
}

