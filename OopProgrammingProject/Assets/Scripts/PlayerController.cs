using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float powerUpStrength = 150f;
    [SerializeField]
    private GameObject powerUpIndicator;
    [SerializeField]
    private GameObject rocketPrefab;

    private Rigidbody playerRb;
    private GameObject focalPoint;//We moved in the direction we were looking using the focal point
    private float forwardInput;

    private bool hasPowerUp = false;
    //PowerUp type is currently empty
    private PowerUpType currentPowerUp = PowerUpType.None;

    //We will use it to spawn new rockets
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    [SerializeField]
    private float hangTime;
    [SerializeField]
    private float smashSpeed;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float explosionRadius;
    private bool smashing = false;
    private float floorY;
    private MainUIHandler mainUIScript;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        mainUIScript = GameObject.Find("MainUIManager").GetComponent<MainUIHandler>();
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        //We equated the position of the ring that appeared around us with the player when we were powerup.
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchRockets();
        }
        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
        if (!mainUIScript.gameOver)
        {
            DeathZone();
        }

    }
    private void DeathZone()
    {
        if (transform.position.y < -10)
        {
            mainUIScript.GameOver();            
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            //Coroutine is a function that performs the hold operation among functions.
            //Here we started Courotine and waited for our call to end.
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerUpCountDownRoutine());
        }
    }
    //IEnumerator is a library that contains a Coroutine and is used for scheduled operations.
    //yield return It is used to return an item inside a loop(for,foreach) without breaking the loop
    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        //hasPowerUp going to false afet 7 second
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
        powerUpIndicator.SetActive(false);
    }
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        //Store the y position before taking off
        floorY = transform.position.y;
        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            //move the player up while still keeping their x velocity.
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        //Now move the player down
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }
        //Cycle through all enemies.
        for (int i = 0; i < enemies.Length; i++)
        {
            //Apply an explosion force that originates from our position.
            if (enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
                transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }
        //We are no longer smashing, so set the boolean to false
        smashing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            //If the player's position is subtracted from the Enemy's position, you will find the vector from the player to the enemy.
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
           // Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }
    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehavior>().Fire(enemy.transform);
        }
    }


}
