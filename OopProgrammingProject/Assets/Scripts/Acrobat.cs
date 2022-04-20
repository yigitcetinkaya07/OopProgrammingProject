using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat : Enemy
{
    protected override void GameStart()
    {
        base.GameStart();
        point = 15;
        SetInvokeTime(5, 8);
        InvokeRepeating("Jump", invokeTime, repeaRate);
    }
    //Acrobat can jump
    private void Jump()
    {
        enemyRb.AddForce(Vector3.up*200, ForceMode.Impulse);
    }
}
