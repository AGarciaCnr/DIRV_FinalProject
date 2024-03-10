using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    private Drink _drink;

    private void Awake()
    {
        _drink = GetComponentInParent<Drink>();
    }

    public bool FallingLiquid()
    {
        // Debug.Log("IsEmpty: " + _drink.IsEmpty() + "\t" + _drink.IsDump());
        return !_drink.IsEmpty() && _drink.IsDumped();
    }

}
