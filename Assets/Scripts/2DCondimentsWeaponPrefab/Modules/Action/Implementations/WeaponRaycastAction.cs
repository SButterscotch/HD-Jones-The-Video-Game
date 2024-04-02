using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using UnityEngine.AI; 

#if UNITY_EDITOR
using UnityEditor; 
using UnityEditorInternal; 
#endif

using Object = UnityEngine.Object; 
using Random = UnityEngine.Random;
using System;

namespace Default {
    public class WeaponRaycaseAction : CondimentsWeapon.Module {

        [SerializeField] Transform point; //point where we fire raycase "bullets"
        [SerializeField] float range = 400f; //400 meters 
        [SerializeField] LayerMask mask = Physics.DefaultRaycastLayers; //assign a layer that protects you from hitting yourself 
        public override void Init()
        {
            base.Init();
            Weapon.Action.OnPerform += Action; //no accessor this time 
        }

        void Action(){ //firing a raycast! 
            if(Physics.Raycast(point.position, point.forward, out var hit, range, mask)){
                Debug.Log(hit.transform); //to tell where we hit 
            }
        }

    }
}
