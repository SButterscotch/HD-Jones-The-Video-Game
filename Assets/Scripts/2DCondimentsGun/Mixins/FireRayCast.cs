/*
* Filename: FireRayCase.cs
* Developer: K Atkinson
* Purpose: Uses MixinBase class
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRayCast : MixinBase 
{
    //Somewhere to fire from 
    public Transform firePosition; 
    public float range; 
    public override void Action()
    {
        RaycastHit hit; 
        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, range)) { 
            print("I hit" + hit.transform); 
        }
        base.Action();
    }
}
