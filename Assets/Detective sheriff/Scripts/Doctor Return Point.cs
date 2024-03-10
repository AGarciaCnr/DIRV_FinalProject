using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorReturnPoint : MonoBehaviour
{
    [SerializeField] private GameObject doctor;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject == doctor)
        {
            doctor.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
