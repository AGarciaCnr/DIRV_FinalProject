using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeGrabbed : MonoBehaviour
{
    public bool axeIsGrabbed = false;
    public bool axeIsUnGrabbedAfterGrabbed = false;

    public void Grabbed()
    {
        axeIsGrabbed = true;
        axeIsUnGrabbedAfterGrabbed = false;
    }

    public void UnGrabbed()
    {
        axeIsGrabbed = false;
        axeIsUnGrabbedAfterGrabbed = true;
    }

}
