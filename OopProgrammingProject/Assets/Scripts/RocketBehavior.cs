using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;//Is target exist

    private float rocketStrength = 250.0f;
    private float aliveTimer = 5.0f;

    void Update()
    {
        //Rocket launch towards the enemy
        if (homing && target != null)
        {
            //If we subtract our position from the enemy's position, the right direction is found for enemy.
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }

    }
    //We ara gonna call inPlayerController
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }
    void OnCollisionEnter(Collision col)
    {
        if (target != null)
        {
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                //We find the vector to which we can take the opposite direction of the collision point and apply the force.
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
