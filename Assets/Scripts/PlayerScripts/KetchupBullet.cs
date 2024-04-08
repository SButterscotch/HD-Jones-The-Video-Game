/*
* Filename: SubClassBullet.cs
* Developer: K Atkinson
* Purpose: Script used to .  
* Attached to what in the inspector? 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupBullet : Bullet
{
   protected override void OnTriggerEnter2D(Collider2D other) { 
    //Custom behavior for KetchupBullet when colliding with objects 
    base.OnTriggerEnter2D(other); 
   } 
}