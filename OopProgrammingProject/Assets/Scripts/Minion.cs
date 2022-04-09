using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    protected override void GameStart()
    {
        base.GameStart();
        SetInvokeTime(3,5);
        InvokeRepeating("Accelerate", invokeTime, repeaRate);
    }
    //Minions can accelerate
    private void Accelerate()
    {
        if (speed>50)
        {
            speed /= 2;
        }
        else
        {
            speed *= 2;
        }
    }
    
}
