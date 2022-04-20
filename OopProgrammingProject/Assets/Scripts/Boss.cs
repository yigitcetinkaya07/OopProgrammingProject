using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// INHERITANCE
public class Boss : Enemy
{
    private SpawnManager spawnManager;
    private int enemyToSpawn = 1;
    // POLYMORPHISM
    protected override void GameStart()
    {
        base.GameStart();
        point = 30;
        SetInvokeTime(1, 5);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        InvokeRepeating("SpawnMinions", invokeTime,repeaRate);
    }
    //Boss can spawn enemy
    private void SpawnMinions()
    {
        spawnManager.SpawnEnemyWave(enemyToSpawn);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 awayFromBoss = collision.transform.position - transform.position;
            Debug.Log("boss collision " + collision.gameObject.name);
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            playerRb.AddForce(awayFromBoss * 30, ForceMode.Impulse);
        }
        
    }
}
