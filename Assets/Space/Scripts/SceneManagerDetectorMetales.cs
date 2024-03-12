using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public TextMeshPro timeText;

    public void win()
    {
        GameManager.Instance.StopTimer();

        timeText.text = ("Tu tiempo: " + GameManager.Instance.getTime().ToString("F2"));
        timeText.gameObject.SetActive(true);     

        GameManager.Instance.GoNextScene();
    }
}
