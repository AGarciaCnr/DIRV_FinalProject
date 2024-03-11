using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheriffDetectiveManager : MonoBehaviour
{
    [SerializeField] private List<Suspect> _suspects;
    [SerializeField] private List<Texture2D> _wantedPosters;
    [SerializeField] private GameObject _posterCanvasImage;

    private bool _gameOver = false;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        foreach (var suspect in _suspects)
        {
            if (suspect.isGuilty && suspect.found)
            {
                _gameOver = true;
                break;
            }
        }

        if (_gameOver)
        {
            Debug.Log("El juego ha terminado");
            GameManager.Instance.GoNextScene();
        }
    }

    void StartGame()
    {
        int randomIndex = Random.Range(0, _suspects.Count);
        _suspects[randomIndex].isGuilty = true;
        _posterCanvasImage.GetComponent<RawImage>().texture = _wantedPosters[randomIndex];
    }
}
