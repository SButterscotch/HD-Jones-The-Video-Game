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
using System.Linq;

namespace Default {
    public class WeaponConstraint : CondimentsWeapon.Module { 
        public List<IInterface> List { get; protected set; } 
        public interface IInterface{ 
            bool Constraint { get; } //Get every constraint attached to weapon, if one constraint is set weapon will not work  
        }

        public bool Active{ //if any constraint is active, return true 
            get { 
                for (int i = 0; i < List.Count; i++) { 
                    if(List[i].Constraint)
                        return true;  
                }
                return false; 
            }
        }
        public override void Configure()
        {
            base.Configure(); { 
                base.Configure(); 
                List = new List<IInterface>(); 
                var selection = Weapon.Behaviors.Where(x=>x is IInterface).Cast<IInterface>(); //every weapon behavior where the weapon implements the interface 
                List.AddRange(selection); 
            }
        }
    }
}
