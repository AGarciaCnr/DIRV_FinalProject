using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunfighterManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefab; // El _prefab que deseas instanciar
    [SerializeField] private int _numberOfInstances = 10; // El número de instancias que deseas crear


    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void Update()
    {

    }

    void StartGame()
    {

        for (int i = 0; i < _numberOfInstances; i++)
        {
            Vector3 randomPosition = RandomNavmeshLocation(10); // Genera una posición aleatoria en el NavMesh
            Instantiate(_prefab, new Vector3(randomPosition.x, randomPosition.y + 0.5f, randomPosition.z), Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)); // Instancia el _prefab en la posición generada

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
}
