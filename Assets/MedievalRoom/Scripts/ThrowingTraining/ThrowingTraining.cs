using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingTraining : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public GameObject objectHatch;
    public List<GameObject> axesGroups = new List<GameObject>();
    public List<GameObject> hatchesGroups = new List<GameObject>();
    public GameObject parentInstantiateGameObject;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    [Header("Hatch Position")]
    public Transform[] hatchesPositions;

    [Header("Camera Position")]
    public Transform camPos;

    public bool TrainingAxeComplete = false;

    bool readyToThrow;

    private void Start()
    {
        SpawnHatches();
    }

    public void ThrowingTraininng()
    {
        GetHatchesStatus();

        if (!TrainingAxeComplete)
        {
            GetAttackPoint();
            GetObjectToThrow();

            if (objectToThrow != null)
            {
                if (objectToThrow.GetComponent<AxeGrabbed>().axeIsUnGrabbedAfterGrabbed)
                {
                    Throw();
                }
            }
        }
    }

    public void SpawnHatches()
    {
        // Instantiate Hatches
        for (int i = 0; i < hatchesPositions.Length; i++)
        {
            if (objectHatch != null)
            {
                GameObject newHatch = Instantiate(objectHatch, hatchesPositions[i].position, hatchesPositions[i].rotation);
                hatchesGroups.Add(newHatch);
                newHatch.transform.parent = parentInstantiateGameObject.transform;
            }
        }
    }

    private void GetAttackPoint()
    {
        foreach (GameObject go in axesGroups)
        {
            if (go.GetComponent<AxeGrabbed>().axeIsGrabbed)
            {
                attackPoint = go.transform;
            }
        }
    }

    private void GetObjectToThrow()
    {
        foreach (GameObject go in axesGroups)
        {
            if (go.GetComponent<AxeGrabbed>().axeIsGrabbed)
            {
                objectToThrow = go;
            }
        }
    }

    private void Throw()
    {
        // get axe to throw
        GameObject projectile = objectToThrow;

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        objectToThrow.GetComponent<AxeGrabbed>().axeIsUnGrabbedAfterGrabbed = false;

        totalThrows--;
    }

    bool GetHatchesStatus()
    {
        foreach (GameObject i in hatchesGroups)
        {
            // Check if the dummy is null
            if (i == null)
            {
                continue; // Skip to the next iteration if the dummy is null
            }

            if (!i.GetComponentInChildren<AxeTarget>().axeTarget)
            {
                TrainingAxeComplete = false;
                return false;
            }
        }

        TrainingAxeComplete = true;
        return true;
    }
}
