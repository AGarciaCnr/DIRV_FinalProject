using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    float rotX;
    float rotY;
    float rotZ;


    // Start is called before the first frame update
    void Start()
    {
        rotX = Random.Range(-0.01f, 0.01f);
        rotY = Random.Range(-0.01f, 0.01f);
        rotZ = Random.Range(-0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotX, rotY, rotZ);
    }
}
