using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{

    float rotX;
    float rotY;
    float rotZ;

    void Start()
    {
        rotX = Random.Range(-0.02f, 0.02f);
        rotY = Random.Range(-0.02f, 0.02f);
        rotZ = Random.Range(-0.02f, 0.02f);
    }

    void Update()
    {
        this.transform.Rotate(rotX, rotY, rotZ);
    }
}
