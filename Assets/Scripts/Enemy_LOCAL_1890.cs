using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    [SerializeField] public float speed = 3f;
    [SerializeField] private float RotateSpeed = 0.0025f;
    private static Enemy _instance;
    public static Enemy Instance { get { return _instance; } }
    private Rigidbody2D rb;
    private int EnemyHealth = 3;
    private Vector3 InitialScale;
    //Meghan Initialization
    AudioManager audioManager;
    public static int totalEnemies; //Total number of enemies in the scene
    private static int enemiesDefeated; //Number of enemies defeated


    //Meghan Function
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        InitialScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }
  
    private void Update()
    {
        //get the target
        if (!target)
        {
            GetTarget();
        }
        else
        {
            
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;

        // Determine whether the target is closer horizontally or vertically
        bool closerHorizontally = Mathf.Abs(targetDirection.x) > Mathf.Abs(targetDirection.y);

        if (closerHorizontally)
        {
            // Align with left or right direction
            if (targetDirection.x > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0); // Right
            else
                transform.rotation = Quaternion.Euler(0, 180, 0); // Left
        }
        else
        {
            // Align with up or down direction
            if (targetDirection.y > 0)
                transform.rotation = Quaternion.Euler(0, 0, 90); // Up
            else
                transform.rotation = Quaternion.Euler(0, 0, -90); // Down
        }
    
    }

    private void FixedUpdate() 
    {
        // Move forwards
        rb.velocity = transform.up * speed;
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
