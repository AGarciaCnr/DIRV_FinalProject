using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDetector : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject m_Detector;
    public GameObject m_Metal;
    public AudioSource m_Pitido;

    int distance;
    int distanceTransformed;
    float frequency;
    float timerBeep;

    void Start()
    {
        distance = 0;
        distanceTransformed = 0;
        frequency = 10;
        timerBeep = 0;

        m_Detector = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs(m_Detector.transform.position.y - m_Metal.transform.position.y) < 0.2)
        RaycastHit hit;

        if (Physics.Raycast(m_Detector.transform.position, Vector3.down, out hit, 0.3f))
        {
            distance = (int)Vector3.Distance(m_Detector.transform.position, m_Metal.transform.position);
            distanceTransformed = distance == 0? 1: (distance - distance%4 + 8) / 4;
            frequency = (float)distanceTransformed / 5f;

            if (timerBeep > frequency)
            {
                m_Pitido.time = 0.5f;
                m_Pitido.Play();
                timerBeep = 0;
            }

            timerBeep += Time.deltaTime;
        }
    }
}
