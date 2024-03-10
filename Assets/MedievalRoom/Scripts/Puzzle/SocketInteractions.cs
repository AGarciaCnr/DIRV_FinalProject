using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractions : MonoBehaviour
{
    public KnightPuzzle getPuzzleScript;

    public XRSocketInteractor socketInteractor;
    public bool SocketIsPlaced = false;
    public bool Grabbed = false;

    private void Update()
    {
        DeactivateSocket();
    }

    public void ActivateSocket()
    {
        if (socketInteractor != null)
        {
            socketInteractor.socketActive = true;
            Grabbed = true;
        }
    }

    public void UnGrabbed()
    {
        if (socketInteractor != null)
        {
            Grabbed = false;
        }
    }

    public void SocketPlaced()
    {
        if (socketInteractor != null)
        {
            SocketIsPlaced = true;
            getPuzzleScript.SocketActiveCounter++;
        }
    }

    public void SocketOutPlace()
    {
        if (socketInteractor != null)
        {
            SocketIsPlaced = false;
            getPuzzleScript.SocketActiveCounter--;
        }
    }

    public void DeactivateSocket()
    {
        if (socketInteractor != null)
        {
            if (SocketIsPlaced == false && Grabbed == false)
            {
                socketInteractor.socketActive = false;
            }
        }
    }
}
