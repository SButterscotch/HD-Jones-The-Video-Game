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
    public class CondimentsWeapon : MonoBehaviour
    {
        public IBehavior[] Behaviors { get; protected set;}
        public interface IBehavior{ 

            //Like Awake 
            void Configure(); 

            //Like Start 
            void Init(); 
    
        }
        public class Behavior : MonoBehaviour, IBehavior { 
            public virtual void Configure(){
                
            }
            public virtual void Init(){ 

            }
        }

        public WeaponAction Action { get; protected set; } //set this later on really easily 
        public WeaponConstraint Constraint { get; protected set; } //set this later on really easily 
        public IModule[] Modules { get; protected set;}
        public interface IModule { 
            void Set(CondimentsWeapon reference); 
        }
        public class Module : Behavior, IModule 
        { 
            public CondimentsWeapon Weapon { get; protected set; }
            public virtual void Set(CondimentsWeapon reference){
                Weapon = reference; 
            }
        }

        public IOwner Owner { get; protected set; }
        public interface IOwner{ //will control the weapon 
            IProcessor[] Processors {get; } 
        }
        public void Setup(IOwner reference){ 
            Owner = reference; 
            Behaviors = GetComponentsInChildren<IBehavior>(true);
            Modules =  GetComponentsInChildren<IModule>(true);

            Action = Modules.First(x=>x is WeaponAction) as WeaponAction; 
            Constraint = Modules.First(x=>x is WeaponConstraint) as WeaponConstraint;

            Array.ForEach(Modules, x=>x.Set(this)); //Call for every single module in the arry 
            Array.ForEach(Behaviors, x=>x.Configure()); 
            Array.ForEach(Behaviors, x=>x.Init());
        }
        void Update(){ 
            Process(); 
        }
        public event Action OnProcess; 
        void Process(){ 
            OnProcess?.Invoke(); 
        }

        //helper function to retrieve processor 
        public T GetProcessor<T>()
            where T : IProcessor {
                for (int i = 0; i < Owner.Processors.Length; i++) { 
                    if(Owner.Processors[i] is T processor) 
                        return processor; 
                }
                throw new Exception($"No Processor of Type {typeof(T)} found"); 
            }
        public interface IProcessor{ 

        }
    
    }
}
