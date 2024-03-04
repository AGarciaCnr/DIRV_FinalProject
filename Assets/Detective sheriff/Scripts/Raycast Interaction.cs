using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    public float maxDistance = 10f;
    public float gazeTime = 0.5f;
    private float _timer;

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * maxDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Estas mirando a " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponentInParent<Suspect>())
            {
                _timer += Time.deltaTime;

                if (_timer >= gazeTime)
                {
                    hit.collider.gameObject.GetComponentInParent<Suspect>().found = true;
                    _timer = 0.0f;
                }
            }
        }
    }
}
