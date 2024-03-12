using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerDetectorMetales : MonoBehaviour
{

    public GameObject[] pos;
    public GameObject metal;

    void Start()
    {
        GameManager.Instance.locateFade();
        GameManager.Instance.StartTimer();

        metal.transform.position = pos[Random.Range(0, pos.Length)].transform.position;
        metal.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    void Update()
    {
        
    }

    public void win()
    {
        GameManager.Instance.StopTimer();
        GameManager.Instance.GoNextScene();
    }
}
