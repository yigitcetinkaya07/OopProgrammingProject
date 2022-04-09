using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float speed = 30f;

    protected Rigidbody enemyRb;
    private GameObject player;

    private Vector3 lookDirection;

    protected int repeaRate = 10;
    protected int invokeTime = 5;
    private void Start()
    {
        GameStart();
    }
    protected virtual void GameStart()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        CheckBoundary();
    }
    protected virtual void Move()
    {
        lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }
    protected virtual void CheckBoundary()
    {

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    protected void SetInvokeTime(int time,int repeat)
    {
        invokeTime = time;
        repeaRate = repeat;
    }
}
