using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AxeAddon : MonoBehaviour
{
    private Rigidbody rb;

    public bool targetHit;

    public LayerMask targetLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get the layer of the collided object
        int collidedLayer = collision.gameObject.layer;
        
        // Check if the collided object's layer matches any of the layers included in the LayerMask
        if (IsLayerInLayerMask(collidedLayer, targetLayer))
        {
            collision.gameObject.GetComponent<AxeTarget>().axeTarget = true;

            // Set targetHit to true since the axe will stick to this target
            targetHit = true;

            // Make sure axe sticks to the surface
            rb.isKinematic = true;

            // Make sure axe moves with target
            transform.SetParent(collision.transform);

            GetComponent<XRGrabInteractable>().enabled = false;
        }
    }

    // Method to check if a layer is included in a LayerMask
    private bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        // Check if the bit at the index of the layer is set in the LayerMask
        return layerMask == (layerMask | (1 << layer));
    }
}
