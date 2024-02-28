using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SimonSaysManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _drinkingObject;
    [SerializeField] private int _numberOfRounds = 4;
    [SerializeField] private List<Drink> _drinksList;
    private List<Drink> _currentSequence = new List<Drink>();
    private int _currentRound;
    private int _prevNumberIndex = -1;

    private bool _playerCompletedAction = false;

    private void Awake()
    {
        foreach (Drink drink in _drinksList)
        {
            UnhighlightDrink(drink);
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        yield return new WaitForSeconds(10f); // Tiempo para que el jugador se prepare
        _currentRound = 0;
        while (_currentRound < _numberOfRounds - 1)
        {
            _currentRound++;
            Debug.Log("Comienza la ronda: " + _currentRound);
            AddRandomDrinkToSequence();

            foreach (Drink drink in _currentSequence)
            {
                Debug.Log("Bebida iluminada: " + drink.name);
                HighlightDrink(drink);
                yield return new WaitForSeconds(1f);
                UnhighlightDrink(drink);
                yield return new WaitForSeconds(0.2f);
            }

            yield return StartCoroutine(CheckPlayerAction());

            if (!_playerCompletedAction)
            {
                // El jugador no realizó la acción correcta
                Debug.Log("¡Has perdido!");
                GameRestart();
                yield break;
            }

            Debug.Log("Ronda completada");

            yield return new WaitForSeconds(1f); // Tiempo entre rondas
        }
        Debug.Log("¡Has ganado!");
    }

    private void AddRandomDrinkToSequence()
    {
        int randomIndex = Random.Range(0, _drinksList.Count);
        while (randomIndex == _prevNumberIndex)
        {
            randomIndex = Random.Range(0, _drinksList.Count);
        }
        _currentSequence.Add(_drinksList[randomIndex]);
        _prevNumberIndex = randomIndex;
        Debug.Log("Bebida añadida a la secuencia: " + randomIndex);
    }

    private void HighlightDrink(Drink drink)
    {
        drink.gameObject.GetComponent<Outline>().enabled = true;
    }

    private void UnhighlightDrink(Drink drink)
    {
        drink.gameObject.GetComponent<Outline>().enabled = false;
    }

    private IEnumerator CheckPlayerAction()
    {
        DrinkingObject drinkingObject = _drinkingObject.GetComponent<DrinkingObject>();
        List<Drink> tempSequence = new List<Drink>(_currentSequence);

        foreach (Drink drinkSeq in tempSequence)
        {
            yield return new WaitForSeconds(5f);
            GameObject drink = drinkingObject._drink;
            if (drink == null) 
            { 
                Debug.Log("NULL");
                _playerCompletedAction = false; 
                yield break;
            }
            if (drinkSeq.gameObject != drink)
            {
                Debug.Log("INCORRECTO: Bebida esperada: " + drinkSeq.gameObject.name + " >> Bebida obtenida: " + drink.name);
                drinkingObject._drink = null;
                _playerCompletedAction = false;
                yield break;
            }
            Debug.Log("CORRECTO: Bebida esperada: " + drinkSeq.gameObject.name + " >> Bebida obtenida: " + drink.name);
            drinkingObject._drink = null;
        }
        _playerCompletedAction = true;
        yield break;
    }

    private void GameRestart()
    {
        _currentSequence.Clear();
        _currentRound = 0;
        StartGame();
    }
}
