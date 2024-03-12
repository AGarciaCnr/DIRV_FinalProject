using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
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
}
