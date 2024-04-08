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
    [RequireComponent(typeof(AudioSource))]
    public class WeaponAudio : CondimentsWeapon.Module 
    {
        public AudioSource Source { get; protected set; }
        public override void Configure()
        {
            base.Configure();

            Source = GetComponent<AudioSource>(); 
        }
    }
}