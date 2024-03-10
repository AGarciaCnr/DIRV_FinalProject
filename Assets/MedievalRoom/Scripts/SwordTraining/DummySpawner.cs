using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySpawner : MonoBehaviour
{
    public GameObject dummyPrefab;
    public Transform[] spawnPoints;

    public void SpawnDummies(int numDummies)
    {
        for (int i = 0; i < numDummies; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPos = spawnPoints[randomIndex].position;
            Instantiate(dummyPrefab, spawnPos, Quaternion.identity);
        }
    }
}