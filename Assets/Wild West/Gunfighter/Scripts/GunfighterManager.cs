using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GunfighterManager : MonoBehaviour
{
    public TextMeshPro timeText;

    [SerializeField] private GameObject _prefab; // El _prefab que deseas instanciar
    [SerializeField] private int _numberOfInstances = 10; // El número de instancias que deseas crear

    private List<Barrel> _barrels; // Lista para llevar un registro de todos los barriles

    private bool _gameOverBarrels = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.locateFade();
        GameManager.Instance.StartTimer();
        StartGame();
    }

    void Update()
    {
        // Comprueba si todos los barriles han sido destruidos
        if (_barrels.Count == 0 && !_gameOverBarrels)
        {
            Debug.Log("Todos los barriles han sido destruidos");
            Debug.Log("¡Has ganado!");
            SceneCompleted();

            _gameOverBarrels = true;
        }
    }

    void StartGame()
    {
        _barrels = new List<Barrel>();
        for (int i = 0; i < _numberOfInstances; i++)
        {
            Vector3 randomPosition = RandomNavmeshLocation(10); // Genera una posición aleatoria en el NavMesh
            GameObject barrelObject = Instantiate(_prefab, new Vector3(randomPosition.x, randomPosition.y + 0.2f, randomPosition.z), Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)); // Instancia el _prefab en la posición generada
            Barrel barrel = barrelObject.GetComponent<Barrel>();
            barrel.SetManager(this);
            _barrels.Add(barrel); // Añade el barril a la lista
        }
    }

    // Método para generar una posición aleatoria en el NavMesh
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    // Método para eliminar un barril de la lista cuando es destruido
    public void BarrelDestroyed(Barrel barrel)
    {
        Debug.Log("Barrel: " + barrel.name + "destroyed");
        _barrels.Remove(barrel);
    }

    private void SceneCompleted()
    {
        GameManager.Instance.StopTimer();

        timeText.gameObject.SetActive(true);
        timeText.text = ("Tu tiempo: " + GameManager.Instance.getTime().ToString("F2"));
        timeText.gameObject.SetActive(true);

        GameManager.Instance.GoNextScene();
    }
}
