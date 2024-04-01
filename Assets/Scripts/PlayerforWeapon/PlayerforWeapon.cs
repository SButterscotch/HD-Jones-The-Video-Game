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
    public class PlayerforWeapon : MonoBehaviour, CondimentsWeapon.IOwner, WeaponAction.IProcessor
    {
        [SerializeField] CondimentsWeapon gun; 
        public CondimentsWeapon.IProcessor[] Processors { get; protected set; }
        public float Action => Input.GetKey(KeyCode.Space) ? 1f : 0f; //space bar to fire raycast 
        void Start() { 
            Processors = GetComponentsInChildren<CondimentsWeapon.IProcessor>(true); 
            gun.Setup(this); //passing in the player as owner 
        }
    }
}