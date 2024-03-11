using System.Collections;
using System.Collections.Generic;
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

    //-----------------------------------------------------------------------------------

    public FadeScreen fadeScreen;
    private int sceneIndex;

    public void locateFade()
    {
        fadeScreen = FindObjectOfType<FadeScreen>();
    }

    public void GoNextScene()
    {
        sceneIndex++;
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Launch the new scene
        SceneManager.LoadScene(sceneIndex);
    }

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
