using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerMedievalRoom : MonoBehaviour
{
    [Header("Training Sections")]
    public SwordTraining swordTrainingScript;
    public ThrowingTraining axeTrainingScript;
    public KnightPuzzle knightPuzzle;

    [Header("Choose the Scene to Run")]
    public bool SwordTrainingRun = false;
    public bool AxeTrainingRun = false;
    public bool KnightPuzzleTrainingRun = false;

    [Header("Settings Times")]
    public float DelayFirstTraining = 0f;
    public float DelaySecondTraining = 0f;
    public float DelayThirdTraining = 0f;

    private bool hasWon = false;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.locateFade();
        // Hide the cursor
        Cursor.visible = false;
    }

    private void StartSwordTraining()
    {
        swordTrainingScript.gameObject.SetActive(true);
        swordTrainingScript.StartSwordTraning();
        //GameManager.Instance.GoNextScene;
    }

    private void StartKnightPuzzle()
    {
        knightPuzzle.gameObject.SetActive(true);
        knightPuzzle.StartPuzzle();
    }
    
    private void StartAxeThrow()
    {
        axeTrainingScript.gameObject.SetActive(true);
        axeTrainingScript.ThrowingTraininng();
    }

    IEnumerator StartGame()
    {
        if (SwordTrainingRun)
        {
            yield return new WaitForSeconds(DelayFirstTraining);
            StartSwordTraining();
        }

        if (AxeTrainingRun)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(DelaySecondTraining);
            StartAxeThrow();
        }

        if (KnightPuzzleTrainingRun)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(DelayThirdTraining);
            StartKnightPuzzle();
        }
    }

    public void Win()
    {
        GameManager.Instance.GoNextScene();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartGame());

        if (swordTrainingScript.TrainingSwordComplete && !hasWon)
        {
            // Call Win() only if TrainingSwordComplete is true and hasWon is false
            Win();
            // Set hasWon to true to indicate that the win condition has been met
            hasWon = true;
        }
    }
}
