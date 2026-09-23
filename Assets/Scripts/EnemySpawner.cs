using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{

    public GameObject asteroidPrefab; //Asteroides a spawnear
    public float spawnRatePerMinute = 30f; //30 asterioides por minuto
    public float spawnRateIncrement = 1f; //incrementa el numero de asteroides que aparecen
    public float xLimit;
    public float maxTimeLife = 4f;
    private float spawnNext = 0;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > spawnNext)
        {
            spawnNext = Time.time + 60/spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;
            float rand = Random.Range(-xLimit, xLimit);
            Vector3 spawnPosition = new Vector3(rand, 11f, 1.74f);
            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            Destroy(meteor, maxTimeLife);
        }
    }
}
