using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //private int currentSceneIndex = 0;
    //private float startTime;
    //private float endTime;

    //private void Start()
    //{
    //    startTime = Time.time;
    //    LoadNextScene();
    //}

    //public void SceneCompleted()
    //{
    //    endTime = Time.time;
    //    float totalTime = endTime - startTime;
    //    float previousBestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

    //    if (totalTime < previousBestTime)
    //    {
    //        PlayerPrefs.SetFloat("BestTime", totalTime);
    //    }

    //    currentSceneIndex++;

    //    if (currentSceneIndex < scenes.Count)
    //    {
    //        LoadNextScene();
    //    }
    //    else
    //    {
    //        Debug.Log("All scenes completed!");
    //        Debug.Log("Best time: " + PlayerPrefs.GetFloat("BestTime"));
    //    }
    //}

    //private void LoadNextScene()
    //{
    //    SceneManager.LoadScene(scenes[currentSceneIndex]);
    //}

    //-------------------------- Cambio de escena ----------------------------------------

    public FadeScreen fadeScreen;
    private int sceneIndex;

    public void locateFade()
    {
        fadeScreen = FindObjectOfType<FadeScreen>();
    }

    public void GoNextScene()
    {
        sceneIndex = (sceneIndex + 1) % 10;
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Launch the new scene
        SceneManager.LoadScene(sceneIndex);
    }

    //--------------------------- Calculadora de tiempos --------------------------------

    private float timeStart;
    private float timeEnd;
    private float sceneTime;
    public float totalTime;

    public void StartTimer()
    {
        timeStart = Time.time;
    }

    public void StopTimer()
    {
        timeEnd = Time.time;
        sceneTime = timeEnd - timeStart;
        totalTime += sceneTime;
        Debug.Log("Total: " + sceneTime);
    }

    public float getTime()
    {
        return sceneTime;
    }

/*    public void saveTime()
    {
        PlayerPrefs.SetFloat("BestTime", totalTime);
    }*/


    //public void GoToSceneAsync(int sceneIndex)
    //{
    //    StartCoroutine(GoToSceneRoutine(sceneIndex));
    //}

    //IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    //{
    //    fadeScreen.FadeOut();

    //    // Launch the new scene
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    //    operation.allowSceneActivation = false;

    //    float timer = 0;
    //    while (timer <= fadeScreen.fadeDuration && !operation.isDone)
    //    {
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    operation.allowSceneActivation = true;
    //}
}
