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
using System.Linq;

namespace Default {
    public class WeaponRPM : CondimentsWeapon.Module, WeaponConstraint.IInterface  
    {
        [SerializeField] int value = 600; //600 rpm 

        public float Delay => 60f / value; //formula for RPM 

        float timer = 0f; 
        public bool Constraint => timer > 0f; 

        public override void Init()
        {
            base.Init();
            Weapon.OnProcess += Process; 
            Weapon.Action.OnPerform += Action; 
        }


        void Process(){ 
            timer = Mathf.MoveTowards(timer, 0f, Time.deltaTime); //move timer value towards zero
        }

        void Action() { 
            timer = Delay; 
        }
    }

}
