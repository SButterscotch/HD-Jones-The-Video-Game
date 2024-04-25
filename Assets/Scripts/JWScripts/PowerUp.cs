using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public PowerUpEffect powerupEffect;
    public float amp;
    public float freq;
    Vector3 initPos;


    public void Start()
    {
        initPos = transform.position;
    }

    public void Update()
    {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq)*amp + initPos.y, 0);
    }

    public HealthBarManager healthBarManager;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //powerupEffect.Apply(other.gameObject);
            //healthBarManager.AddHealth(100 - healthBarManager.currentHealth);
            HealthBar healthBar = other.GetComponentInChildren<HealthBar>();
            HealthBarManager healthBarManager = other.GetComponentInChildren<HealthBarManager>(); 
            if (healthBar != null)
            {
                //healthBar.ApplyPowerUp();
                healthBarManager.ApplyPowerUp(); 
                Debug.Log($"Current health is {healthBarManager.currentHealth}"); 
                //healthBarManager.currentHealth = 100;
            }
            Destroy(gameObject); // Destroy the power-up object after it has been collected
        }
    }
}
