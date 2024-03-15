using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MainLevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro bestScoreText;
    [SerializeField] private GameObject ball;

    void Start()
    {
        if (GameManager.Instance.totalTime > 1.00f)
        {
            if (GameManager.Instance.totalTime > PlayerPrefs.GetFloat("BestTime", 0))
            {
                PlayerPrefs.SetFloat("BestTime", GameManager.Instance.totalTime);
            }
            ball.GetComponent<XRGrabInteractable>().enabled = true;
        }

        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestScoreText.text = PlayerPrefs.GetFloat("BestTime", 0).ToString("F2") + " seconds";
        }
        else
        {
            bestScoreText.text = "0.00 seconds";
        }

        GameManager.Instance.totalTime = 0.0f;
    }
}
