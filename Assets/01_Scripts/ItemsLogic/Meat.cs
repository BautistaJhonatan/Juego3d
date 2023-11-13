using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meet : BaseItem
{
    [SerializeField] float healtRestoureAmount =10;
    public override void Use()
    {
        Debug.Log($"Healt increased:{healtRestoureAmount }HP");
    }

    
}
