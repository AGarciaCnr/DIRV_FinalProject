using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballEffect : MonoBehaviour
{

    Renderer ball;
    bool isEffect = false;
    float tiempo = -1;

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEffect)
        {
            ball.material.SetFloat("_time", tiempo);
            tiempo += Time.deltaTime;
        }
    }

    public void startEffect()
    {
        isEffect = true;
    }




}
