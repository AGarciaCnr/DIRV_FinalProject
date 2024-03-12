using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerRockLeaper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.locateFade();
        GameManager.Instance.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void win()
    {
        GameManager.Instance.StopTimer();
        GameManager.Instance.GoNextScene();
    }
}
