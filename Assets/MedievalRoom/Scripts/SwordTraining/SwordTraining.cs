using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTraining : MonoBehaviour
{
    [Header("References")]
    public GameObject prefabDummy;
    public Transform cameraPos;

    [Header("Settings")]
    public float dummyDestroyDelay = 4f;
    public float levelWaitTime = 4f;

    [Header("Levels Transform")]
    public Transform level01Transform;
    public Transform level02Transform;
    public Transform level03Transform;

    [Header("Camera Position by Level")]
    public Transform camPostLevel01;
    public Transform camPostLevel02;
    public Transform camPostLevel03;

    [Header("Dummies Positions")]
    public Transform[] positionLevel01; // Positions for level 1
    public Transform[] positionLevel02; // Positions for level 2
    public Transform[] positionLevel03; // Positions for level 2

    public bool isDestroyingDummies = false;
    public bool isSlicedComplete = false;

    public bool SwordLevel01 = false;
    public bool SwordLevel02 = false;
    public bool SwordLevel03 = false;
    public bool TrainingSwordComplete = false;

    [Header("Current Level")]
    public int currentLevel = 1;

    // List to keep track of active dummies
    public List<GameObject> activeDummies = new List<GameObject>();
    public GameObject activeSlicedDummiesGrp;

    private void Awake()
    {
        SpawnDummies();
    }
    private void Start()
    {
        if (activeDummies.Count > 0)
        {
            Invoke("DelayedFadeIn", 0.1f);
        }
    }

    public void SpawnDummies()
    {
        // Choose positions based on current level
        Transform[] currentLevelPositions = GetCurrentLevelPositions();

        // Instantiate Dummies
        for (int i = 0; i < currentLevelPositions.Length; i++)
        {
            GameObject newDummy = Instantiate(prefabDummy, currentLevelPositions[i].position, currentLevelPositions[i].rotation);
            activeDummies.Add(newDummy); // Add the instantiated dummy to the list of active dummies
            Animator newDummyAnim = newDummy.GetComponent<Animator>();

            switch (currentLevel)
            {
                case 1:
                    if (cameraPos != null)
                    {
                        cameraPos.GetComponentInChildren<PlayerController>().target = camPostLevel01.transform;
                        cameraPos.GetComponentInChildren<PlayerController>().Recenter();
                    }

                    newDummy.transform.parent = level01Transform;

                    break;
                case 2:
                    newDummy.transform.parent = level02Transform;
                    newDummyAnim.SetBool("isMoving", true);

                    if (cameraPos != null)
                    {
                        cameraPos.GetComponentInChildren<PlayerController>().target = camPostLevel02.transform;
                        cameraPos.GetComponentInChildren<PlayerController>().Recenter();
                    }

                    break;
                case 3:
                    newDummy.transform.parent = level03Transform;

                    if (cameraPos != null)
                    {
                        cameraPos.GetComponentInChildren<PlayerController>().target = camPostLevel03.transform;
                        cameraPos.GetComponentInChildren<PlayerController>().Recenter();
                    }

                    if (i % 2 == 0)
                    {
                        AssignAnimatorController(newDummy, "Dummy_GRP");
                        newDummyAnim.SetBool("isMoving", true);
                    }
                    else
                    {
                        AssignAnimatorController(newDummy, "Dummy_GRPInv");
                        newDummyAnim.SetBool("isMovingInverse", true);
                    }

                    break;
                default:
                    break;
            }
        }

        if (activeDummies.Count > 0)
        {
            Invoke("DelayedFadeIn", 0.1f);
        }
    }

    private void DelayedFadeIn()
    {
        dummyFadeIn();
    }

    void dummyFadeIn()
    {
        MakeTransparent[] SlicedDummies = activeSlicedDummiesGrp.GetComponentsInChildren<MakeTransparent>();

        foreach (GameObject dummy in activeDummies)
        {
            MakeTransparent[] dummyFade = dummy.GetComponentsInChildren<MakeTransparent>();

            foreach (MakeTransparent d in dummyFade)
            {
                d.FadeIn(2.0f, 1.0f);
            }
        }

        foreach (MakeTransparent d in SlicedDummies)
        {
            d.FadeIn(2.0f, 1.0f);
        }
    }
    
    void dummyFadeOut()
    {
        MakeTransparent[] SlicedDummies = activeSlicedDummiesGrp.GetComponentsInChildren<MakeTransparent>();

        foreach (GameObject dummy in activeDummies)
        {
            MakeTransparent[] dummyFade = dummy.GetComponentsInChildren<MakeTransparent>();

            foreach (MakeTransparent d in dummyFade)
            {
                d.FadeOut(2f, 1f);
            }
        }


        foreach (MakeTransparent d in SlicedDummies)
        {
            d.FadeOut(2.0f, 1.0f);
        }
    }

    Transform[] GetCurrentLevelPositions()
    {
        switch (currentLevel)
        {
            case 1:
                return positionLevel01;
            case 2:
                return positionLevel02;
            case 3:
                return positionLevel03;
            default:
                return new Transform[0];
        }
    }

    // Call this method whenever the player completes a level
    void AdvanceToNextLevel()
    {
        currentLevel++;
        SpawnDummies();
    }

    // Check if all active dummies are sliced
    bool AreAllDummiesSliced()
    {
        foreach (GameObject dummy in activeDummies)
        {   
            // Check if the dummy is null
            if (dummy == null)
            {
                continue; // Skip to the next iteration if the dummy is null
            }

            // Get the DummyObject component attached to the dummy
            DummyObject dummyObject = dummy.GetComponent<DummyObject>();

            // If the DummyObject component exists and the dummy is not sliced, return false
            if (dummyObject != null && !dummyObject.isSliced)
            {
                return false;
            }
        }

        isSlicedComplete = true;

        // If all dummies are sliced, return true
        return true;
    }

    IEnumerator DestroyDummiesAndAdvance()
    {
        // Set the flag to indicate the coroutine is running
        isDestroyingDummies = true;

        isSlicedComplete = false;

        dummyFadeOut();

        // Wait for the specified delay
        yield return new WaitForSeconds(dummyDestroyDelay);

        // Destroy all dummies
        foreach (GameObject dummy in activeDummies)
        {
            Destroy(dummy);
        }

        // Destroy all dummies
        for (int i = 0; i < activeSlicedDummiesGrp.transform.childCount; i++)
        {
            GameObject child = activeSlicedDummiesGrp.transform.GetChild(i).gameObject;

            Destroy(child);
        }

        activeDummies.Clear();

        cameraPos.GetComponentInChildren<FadeScreen>().FadeOut();

        // Wait for the specified delay
        yield return new WaitForSeconds(2f);

        // Advance to the next level
        AdvanceToNextLevel();

        cameraPos.GetComponentInChildren<FadeScreen>().FadeIn();

        // Reset the flag when coroutine finishes
        isDestroyingDummies = false;
    }

    void AssignAnimatorController(GameObject target, string controllerName)
    {
        Animator animator = target.GetComponent<Animator>();
        if (animator != null)
        {
            RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(controllerName);
            if (controller != null)
            {
                animator.runtimeAnimatorController = controller;
            }
            else
            {
                Debug.LogWarning("Animator Controller named " + controllerName + " not found!");
            }
        }
        else
        {
            Debug.LogWarning("Animator Component not found!");
        }
    }

    public void StartSwordTraning()
    {
        if (!TrainingSwordComplete)
        {
            // Check if a coroutine is already running
            if (!isDestroyingDummies && !isSlicedComplete)
            {
                // Check if all dummies are sliced
                if (AreAllDummiesSliced())
                {
                    // If all dummies are sliced, start coroutine to destroy them with a delay
                    StartCoroutine(DestroyDummiesAndAdvance());

                    if (currentLevel == 1)
                    {
                        SwordLevel01 = true;
                    }
                    else if (currentLevel == 2)
                    {
                        SwordLevel02 = true;
                    }
                    else if (currentLevel == 3)
                    {
                        SwordLevel03 = true;
                        TrainingSwordComplete = true;
                    }
                }
            }
        }
    }

}