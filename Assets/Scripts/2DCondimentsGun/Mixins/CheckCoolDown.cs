//Make sure the weapon doesn't fire every frame 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCoolDown : MixinBase
{
    public float cooldownTimer; 
    float cooldownTime; 
    bool isCool = true; 
    //Override the check function 
    public override bool Check(){ 
        return isCool; 
    }
    public override void Action()
    {
        isCool = false;
        cooldownTime = 0.0f; 
    }

    public void Update() { 
        if(!isCool){ 
            cooldownTime += Time.deltaTime; 
            if(cooldownTime > cooldownTimer) {
                isCool = true; 
            }
        }
    }
}
