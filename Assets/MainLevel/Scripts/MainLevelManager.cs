using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainLevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro bestScoreText;

    void Start()
    {
        if (GameManager.Instance.totalTime > 1.00f)
        {
            if (GameManager.Instance.totalTime > PlayerPrefs.GetFloat("BestTime", 0))
            {
                PlayerPrefs.SetFloat("BestTime", GameManager.Instance.totalTime);
            }
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
