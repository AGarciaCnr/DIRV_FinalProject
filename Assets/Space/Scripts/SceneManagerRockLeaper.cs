using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManagerRockLeaper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.locateFade();
        GameManager.Instance.StartTimer();
    }

    public TextMeshPro timeText;

    public void win()
    {
        GameManager.Instance.StopTimer();

        timeText.gameObject.SetActive(true);
        timeText.text = ("Tu tiempo: " + GameManager.Instance.getTime().ToString("F2"));
        timeText.gameObject.SetActive(true);

        GameManager.Instance.GoNextScene();
    }
}
