using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/Landing")]
    public class Landing : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}