using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    public class CharacterStateBase : StateMachineBehaviour
    {

        private CharacterControl characterControl; // Every state will be able to access the characterControl script
        public CharacterControl GetCharacterControl(Animator animator)
        {

            if (characterControl == null) // if the private variable is empty
            {
                characterControl = animator.GetComponentInParent<CharacterControl>(); // get it from the animator
            }
            return characterControl; // otherwise, just return it 

        }


    }
}
   