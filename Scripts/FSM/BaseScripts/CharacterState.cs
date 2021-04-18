using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    public class CharacterState  : StateMachineBehaviour
    {
        public List<StateData> ListAbilityData = new List<StateData>(); // This script will contains a list of the state data 


        public void UpdateAll(CharacterState characterStateBase, Animator animator, AnimatorStateInfo stateInfo) // We want this to go through the list and update every single one of them
        {
            foreach (StateData d in ListAbilityData)
            {
                d.UpdateAbility(characterStateBase, animator, stateInfo);
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // Update it every single frame
        {
            UpdateAll(this, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnExit(this, animator, stateInfo);
            }
        }

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
   