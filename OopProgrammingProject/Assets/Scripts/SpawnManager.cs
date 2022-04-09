using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefab;
    [SerializeField]
    private GameObject bossPrefab;
    [SerializeField]
    private GameObject[] powerUpPrefabs;

    private float spawnRange = 9f;
    private int randomEnemy;

    private int enemyCount;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        //We got the number of objects with Enemy Script on them, pay attention to the s symbol
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            if (waveNumber % 3 != 0)
            {
                waveNumber++;
                SpawnEnemyWave(waveNumber);
                SpawnPowerUp();
            }
            else
            {
                waveNumber++;
                SpawnBossWave();
                SpawnPowerUp();
            }
        }
    }
    public void SpawnEnemyWave(int enemyToSpawn)
    {
        for (int i = 0; i < enemyToSpawn; i++)
        {
            randomEnemy = GenerateRandomNumber(0, 3);
            Instantiate(enemyPrefab[randomEnemy], GenerateRandomPos(), enemyPrefab[randomEnemy].transform.rotation);
        }
    }
    private void SpawnBossWave()
    {
        //randomEnemy = GenerateRandomNumber(0, 3);
       // Instantiate(enemyPrefab[randomEnemy], GenerateRandomPos(), enemyPrefab[randomEnemy].transform.rotation);
        Instantiate(bossPrefab, GenerateRandomPos(), bossPrefab.transform.rotation);
    }
    private void SpawnPowerUp()
    {
        int randomPowerup = GenerateRandomNumber(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[randomPowerup], GenerateRandomPos(), powerUpPrefabs[randomPowerup].transform.rotation);
    }
    private Vector3 GenerateRandomPos()
    {
        float spawnPosX = GenerateRandomNumber(-spawnRange, spawnRange);
        float spawnPosZ = GenerateRandomNumber(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    //To use the generic method in Unity editor go Edit->Project Setting and change Scripting Backend IL2CPP
    private T GenerateRandomNumber<T>(T min, T max)
    {
        dynamic temp1 = min;
        dynamic temp2 = max;
        return Random.Range(temp1, temp2);
    }

}
