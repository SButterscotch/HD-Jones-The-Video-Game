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
    public class WeaponAction : CondimentsWeapon.Module {
        [SerializeField] float minInput = 0.1f; 
        public IProcessor Processor { get; protected set; } //get the entity that owns the processor 
        public interface IProcessor : CondimentsWeapon.IProcessor { 
            float Action { get; } //modular approach to Input.GetInputKey etc, made it a float to deal with joysticks 

        }

        //accessor for constraint 
        public WeaponConstraint Constraint => Weapon.Constraint; 
        public override void Init()
        {
            base.Init(); 

            Processor = Weapon.GetProcessor<IProcessor>(); 

            Weapon.OnProcess += Process; 
        }

        void Process(){ 
            if(Processor.Action > minInput) { //minimum input to achieve action from weapon 
                //Weapon constraint time! 
                if(Constraint.Active == false){ //no constraint, good to go  
                    Perform(); 
                }
            }
        }
        public event Action OnPerform; 
        void Perform(){ 
            OnPerform?.Invoke(); 
        }
    }
}
