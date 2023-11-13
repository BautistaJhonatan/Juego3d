using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : BaseItem
{
    [SerializeField] float DrinkWaterRestoureAmount = 10;
    public override void Use()
    {

        Debug.Log($"You drank:{DrinkWaterRestoureAmount}");
    }

    
}
