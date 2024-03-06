using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody bulletRigidbody))
        {
            this.gameObject.SetActive(false);
        }
    }
}
