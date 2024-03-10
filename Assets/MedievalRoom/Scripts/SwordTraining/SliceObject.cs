using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;

    public LayerMask sliceableLayer;
    public Material crossSectionMaterial;
    public float cutForce = 300f;
    public float cutRadius = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        Debug.DrawLine(startSlicePoint.position, endSlicePoint.position, hasHit ? Color.red : Color.green);

        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null) 
        {
            // Upper Hull
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            // Add CutForce
            SetupSliceComponent(upperHull);

            // Lower Hull
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSliceComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSliceComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = rb.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, cutRadius);
    }
}
