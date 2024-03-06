using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public int maxSpeed = 5;
    public int speedMultiplier = 10;

    public Rigidbody rb;

    public bool isLeftJetpack = false;
    public bool isRightJetpack = false;


    void Start()
    {
    }

    void Update()
    {
        if(isLeftJetpack)
        {
            rb.AddForce(rb.transform.right/8 * -speedMultiplier);
            rb.AddForce(rb.transform.forward/4 * speedMultiplier);
            rb.AddForce(rb.transform.up * speedMultiplier);
        }

        if(isRightJetpack)
        {
            rb.AddForce(rb.transform.right/8 * speedMultiplier);
            rb.AddForce(rb.transform.forward/4 * speedMultiplier);
            rb.AddForce(rb.transform.up * speedMultiplier);
        }

        if (rb.velocity.x > (maxSpeed / 2))
            rb.velocity = new Vector3(maxSpeed / 2, rb.velocity.y, rb.velocity.z);

        if (rb.velocity.x < (-maxSpeed / 2))
            rb.velocity = new Vector3(-maxSpeed / 2, rb.velocity.y, rb.velocity.z);

        if (rb.velocity.z > maxSpeed)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);

        if (rb.velocity.y < -maxSpeed)
            rb.velocity = new Vector3(rb.velocity.x, -maxSpeed, rb.velocity.z);

        if (rb.velocity.y > maxSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed, rb.velocity.z);


    }

    public void left_jetpack()
    {
        isLeftJetpack = !isLeftJetpack;
    }

    public void right_jetpack()
    {
        isRightJetpack = !isRightJetpack;
    }
}
