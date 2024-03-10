using UnityEngine;
using EzySlice;
using UnityEngine.Rendering.Universal;
public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
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

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject HullsGameObject = new GameObject("HullGameObject");

                upperHullGameobject.transform.parent = HullsGameObject.transform;
                lowerHullGameobject.transform.parent = HullsGameObject.transform;

                // UpperHull same position as objectbesliced
                upperHullGameobject.transform.position = objectToBeSliced.transform.position;

                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                // Parent from objectToBeSliced

                Transform parentObjectTobeSliced = objectToBeSliced.transform.parent.gameObject.transform;

                // Parent the upper and lower hull to objectTobeSliced Parent GameObject
                HullsGameObject.transform.parent = parentObjectTobeSliced;

                // Get Parent Object and add DummyObject, then Check Hulls is true
                DummyObject dummyGroup = objectToBeSliced.transform.parent.gameObject.GetComponent<DummyObject>();

                if (dummyGroup != null)
                {
                    dummyGroup.isSliced = true;
                }

                // Make it Physical
                MakeItPhysical(upperHullGameobject);

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
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}