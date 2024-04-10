using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private float Distance;
    [SerializeField] public float DistanceBetween = 10f;
    [SerializeField] public float speed = 3f;
    private static Enemy _instance;
    public static Enemy Instance { get { return _instance; } }
    private Rigidbody2D rb;
    private int EnemyHealth = 3;
    private Vector3 InitialScale;
    //Meghan codeline
    public AudioSource enemyNoise;
    public AudioSource championSound;
    public static int totalEnemies; //Total number of enemies in the scene
    private static int enemiesDefeated; //Number of enemies defeated
    


    private void Start()
    {
        InitialScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        GetTarget(); // Finds player upon entry
        DistanceBetween = 4f;
        //Meghan code
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            enemyNoise = audioSources[0];
            championSound = audioSources[1];
        }
    }

    //Meghan code
    private void OnDestroy()
    {
        // Decrement totalEnemies when an enemy is destroyed
        totalEnemies--;

        // Check if all enemies are defeated
        if (totalEnemies == 0)
        {
            // Play the champion sound
            if (championSound != null)
            {
                championSound.Play();
            }
        }
    }

    private void Update()
    {
        if (target == null)
        {
            GetTarget();
        }

        Distance = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        Vector2 Direction = target.transform.position - transform.position;

        if (Distance < DistanceBetween) {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed = Time.deltaTime);
        }
    }
    
    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // healthbar.SetHealth(health - 10);
            Destroy(other.gameObject);
            target = null;

        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyHealth--;
            //Meghan code
            enemyNoise.Play();
            Debug.Log("Bullet Hit");

            Destroy(other.gameObject);
            if (EnemyHealth == 0)
            {
                Destroy(gameObject);
            } 
        }  
    }

    //K's code in David's Enemy.cs script to kill Enemy with weapon after hit by two (changeable) bullets, called in her Bullet.cs script
    /*Starts here*/ 
    private int bulletsHit = 0;
    [SerializeField]
    private int bulletsToDestroy = 2; //This is changeable, can increase if more bullets need to destroy enemy 

    public void HitByBullet()
    {
        bulletsHit++;
    }

    public bool IsDestroyed()
    {
        return bulletsHit >= bulletsToDestroy;
    }
    /* Ends here */ 
}
