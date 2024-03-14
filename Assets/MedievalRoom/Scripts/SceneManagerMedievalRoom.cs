using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManagerMedievalRoom : MonoBehaviour
{
    public TextMeshPro timeText;

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

    public SoundManager soundManager;
    public FadeScreen fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.locateFade();
        GameManager.Instance.StartTimer();
        // Hide the cursor
        Cursor.visible = false;
    }

    private void StartSwordTraining()
    {
        swordTrainingScript.gameObject.SetActive(true);

        if (!swordTrainingScript.TrainingSwordComplete)
        {
            swordTrainingScript.StartSwordTraning();
        }
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

            StartCoroutine(soundManager.PlayClip(0, 0)); // Welcome Message

            StartSwordTraining();
        }

        if (AxeTrainingRun)
        {
            StartCoroutine(soundManager.PlayClip(0, 0)); // Welcome Message

            // Wait for the specified delay
            yield return new WaitForSeconds(DelaySecondTraining);

            StartAxeThrow();
        }

        if (KnightPuzzleTrainingRun)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(DelayThirdTraining);

            StartCoroutine(soundManager.PlayClip(0, 0)); // Welcome Message

            StartKnightPuzzle();
        }
    }

    public void Win()
    {
        GameManager.Instance.StopTimer();

        timeText.text = ("Tu tiempo: " + GameManager.Instance.getTime().ToString("F2"));
        timeText.gameObject.SetActive(true);

        fadeScreen.fadeDuration = 3;

        GameManager.Instance.GoNextScene();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartGame());

        if (swordTrainingScript.TrainingSwordComplete && !hasWon)
        {
            StartCoroutine(soundManager.PlayClip(3, 0)); // Welcome Message

            // Call Win() only if TrainingSwordComplete is true and hasWon is false
            Win();
            // Set hasWon to true to indicate that the win condition has been met
            hasWon = true;
        }

        if (axeTrainingScript.TrainingAxeComplete && !hasWon)
        {
            StartCoroutine(soundManager.PlayClip(1, 0)); // Welcome Message

            // Call Win() only if TrainingSwordComplete is true and hasWon is false
            Win();
            // Set hasWon to true to indicate that the win condition has been met
            hasWon = true;
        }

        if (knightPuzzle.PuzzleComplete && !hasWon)
        {
            StartCoroutine(soundManager.PlayClip(1, 0)); // Welcome Message
            // Call Win() only if TrainingSwordComplete is true and hasWon is false
            Win();
            // Set hasWon to true to indicate that the win condition has been met
            hasWon = true;
        }
    }
}
