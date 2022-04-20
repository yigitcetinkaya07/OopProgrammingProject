using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Enemy
{
   
    private float areaRange = 9f;
    protected override void GameStart()
    {
        base.GameStart();
        point = 20;
        SetInvokeTime(5, 10);
        InvokeRepeating("Teleport", invokeTime, repeaRate);
    }
    //Ninja can teleport
    private void Teleport()
    {
        if (transform.position.y>=-1)
        {
            float PosX = Random.Range(-areaRange, areaRange);
            float PosZ = Random.Range(-areaRange, areaRange);
            Vector3 randomPos = new Vector3(PosX, 0, PosZ);
            gameObject.SetActive(false);
            transform.position = randomPos;
            enemyRb.velocity = Vector3.zero;
            gameObject.SetActive(true);
        }
       
    }
}
