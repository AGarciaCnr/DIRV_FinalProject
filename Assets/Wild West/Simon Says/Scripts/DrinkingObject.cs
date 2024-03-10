using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingObject : MonoBehaviour
{
    [NonSerialized] public GameObject _drink = null;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Drink Liquid"))
        {
            //Debug.Log("Drink Liquid: " + other.name);
            _drink = other.GetComponentInParent<Drink>().gameObject;
        }
    }
}
