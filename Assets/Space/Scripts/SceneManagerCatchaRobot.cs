using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerCatchaRobot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.locateFade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void win()
    {
        GameManager.Instance.GoNextScene();
    }
}
