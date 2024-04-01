/*
* Filename: CallMixinAction.cs
* Developer: K Atkinson
* Purpose: Script used to iterate through all Mixins in the game and perform a desired action. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMixinAction : MonoBehaviour
{
    public List<MixinBase> actionsMixins; 

    public void CallActions(){ 
        for(int i = 0; i < actionsMixins.Count; i++) { 
            actionsMixins[i].Action(); //for each Mixin, call its action 
        }
    }
}
