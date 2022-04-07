using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 30f;
    private Rigidbody enemyRb;
    private GameObject player;
    private Vector3 lookDirection;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBoundary();
    }
    public void Move()
    {
        lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }
    public void CheckBoundary()
    {

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
