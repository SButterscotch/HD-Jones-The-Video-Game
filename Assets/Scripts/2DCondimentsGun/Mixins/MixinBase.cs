/*
* Filename: MixinBase.cs
* Developer: K Atkinson
* Purpose: Script used for all objects to derive from, contains virtual Action().   
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixinBase : MonoBehaviour
{
   public virtual void Action() { //When we call Action, our desired Mixin will perform the action we want 

   }
}
