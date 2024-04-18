using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerupEffect;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        powerupEffect.Apply(collision.gameObject);
    }
}
