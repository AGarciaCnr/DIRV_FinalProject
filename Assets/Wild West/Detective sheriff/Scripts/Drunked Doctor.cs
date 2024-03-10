using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkedDoctor : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("RotateGameObject", 0f, 15f);
    }

    private void RotateGameObject()
    {
        transform.Rotate(Vector3.up, 180f);
    }
}
