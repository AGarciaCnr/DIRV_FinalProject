using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<string> scenes;
    private int currentSceneIndex = 0;
    private float startTime;
    private float endTime;

    private void Start()
    {
        startTime = Time.time;
        LoadNextScene();
    }

    public void SceneCompleted()
    {
        endTime = Time.time;
        float totalTime = endTime - startTime;
        float previousBestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (totalTime < previousBestTime)
        {
            PlayerPrefs.SetFloat("BestTime", totalTime);
        }

        currentSceneIndex++;

        if (currentSceneIndex < scenes.Count)
        {
            LoadNextScene();
        }
        else
        {
            Debug.Log("All scenes completed!");
            Debug.Log("Best time: " + PlayerPrefs.GetFloat("BestTime"));
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(scenes[currentSceneIndex]);
    }
}
