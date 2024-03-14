using UnityEngine;
using EzySlice;
using UnityEngine.Rendering.Universal;
public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public GameObject parentObjectSliced;
    public LayerMask sliceMask;
    public bool isTouched; 

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                // Parent from objectToBeSliced
                Transform parentObjectTobeSliced = objectToBeSliced.transform.parent.gameObject.transform;

                // Parent GameObject
                GameObject HullsGameObject = new GameObject("HullGameObject");

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                // Parent the upper and lower hull to objectTobeSliced Parent GameObject
                HullsGameObject.transform.parent = parentObjectSliced.transform;

                // Reset the local pos and rotation of HullsGameObject to zero
                HullsGameObject.transform.localPosition = Vector3.zero;
                HullsGameObject.transform.localRotation = Quaternion.identity;
                HullsGameObject.transform.localScale = new Vector3(1,1,1);

                upperHullGameobject.transform.parent = HullsGameObject.transform;
                lowerHullGameobject.transform.parent = HullsGameObject.transform;

                // UpperHull same position as objectbesliced
                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                upperHullGameobject.transform.rotation = objectToBeSliced.transform.rotation;

                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.rotation = objectToBeSliced.transform.rotation;

                // Get Parent Object and add DummyObject, then Check Hulls is true
                DummyObject dummyGroup = objectToBeSliced.transform.parent.gameObject.GetComponent<DummyObject>();

                if (dummyGroup != null)
                {
                    dummyGroup.isSliced = true;
                }

                // Make it Physical
                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                // Destroy objectToBeSliced
                Destroy(objectToBeSliced.gameObject);

                // Attach MakeTransparent script to upperHullGameObject and call FadeOut
                upperHullGameobject.AddComponent<MakeTransparent>();
                lowerHullGameobject.AddComponent<MakeTransparent>();
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        Rigidbody objRigidBody = obj.AddComponent<Rigidbody>();
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // Check if the slicing is occurring along the X-axis
        bool isXAxisSlice = Mathf.Abs(transform.right.x) > Mathf.Abs(transform.right.y);

        // Determine the position of the slicing plane based on a point representing the sharper part of the sword
        Vector3 slicingPosition = transform.position;

        // Use the X-axis as the normal of the slicing plane if the slicing occurs along the X-axis,
        // otherwise use the Y-axis
        Vector3 slicingNormal = transform.up;

        // Log whether the slicing is along the X-axis or the Y-axis
        if (isXAxisSlice)
        {
            Debug.Log("Slicing along the X-axis");

        }
        else
        {
            Debug.Log("Slicing along the Y-axis");
        }

        // Perform the slicing operation using the determined position and normal
        return obj.Slice(slicingPosition, slicingNormal, crossSectionMaterial);
    }
}