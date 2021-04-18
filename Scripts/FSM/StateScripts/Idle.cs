using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/Idle")]
    public class Idle : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
           
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);


            if (control.Jump)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
            }

            if (control.moveRight)
            {

                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true); // if the character is moving set the boolean to true

            }

            if (control.moveLeft)
            {

                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
            }
            
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}