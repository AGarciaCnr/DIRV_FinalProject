using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KnightPuzzle : MonoBehaviour
{
    public int SocketActiveCounter = 0;
    public List<GameObject> SocketObjects = new List<GameObject>();
    public InteractionLayerMask newInteractionLayerMask;

    public void StartPuzzle()
    {
        foreach (GameObject o in SocketObjects)
        {
            XRSocketInteractor GetGrabInteractable = o.GetComponent<XRSocketInteractor>();

            GetGrabInteractable.interactionLayers = newInteractionLayerMask;
        }
    }
}
