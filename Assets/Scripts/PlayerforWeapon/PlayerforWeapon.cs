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
    public class PlayerforWeapon : MonoBehaviour, CondimentsWeapon.IOwner
    {
        [SerializeField] CondimentsWeapon gun; 
        public CondimentsWeapon.IProcessor[] Processors { get; protected set; }
        void Start() { 
            Processors = GetComponentsInChildren<CondimentsWeapon.IProcessor>(true); 
            gun.Setup(this); //passing in the player as owner 
        }
    }
}