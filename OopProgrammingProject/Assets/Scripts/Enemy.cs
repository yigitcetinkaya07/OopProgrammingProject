using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float speed = 30f;

    protected Rigidbody enemyRb;
    private GameObject player;

    private Vector3 lookDirection;

    protected int repeaRate = 10;
    protected int invokeTime = 5;
    private MainUIHandler mainUIScript;
    protected int point = 10;
    //public UnityEvent<int> onDestroyed;
    private void Start()
    {
        GameStart();
    }
    protected virtual void GameStart()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        mainUIScript = GameObject.Find("MainUIManager").GetComponent<MainUIHandler>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (!mainUIScript.gameIsPaused || !mainUIScript.gameOver)
        {
            Move();
            CheckBoundary();
        }
       
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
            mainUIScript.AddPoint(point);
            Destroy(gameObject);
        }
    }
    protected void SetInvokeTime(int time,int repeat)
    {
        invokeTime = time;
        repeaRate = repeat;
    }
}
