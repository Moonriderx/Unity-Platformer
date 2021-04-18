using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider {
    public abstract class StateData : ScriptableObject // it is going to inherit from scriptable object since we want to use scriptable objects to make a small system for the animator behavior
        // The class is abstract since it is going to be the base class of our all pre defined actions. It won't be able to initialize. Blueprint like

    {
        public float Duration; // Every state will have duration

        public abstract void UpdateAbility(CharacterState characterStateBase, Animator animator); // Each state will update based on pre-defined cards (objects) It will take characterStateBase and animator
        

    }
}

