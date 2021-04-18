using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/ForceTransition")]
    public class ForceTransition : StateData
    {
        [Range(0.01f, 1f)]
        public float TransitionTiming; // It is going to be a % value. This variable will determine when we switch to another animation

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= TransitionTiming)
            {
                animator.SetBool(CharacterControl.TransitionParameter.ForceTransition.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(CharacterControl.TransitionParameter.ForceTransition.ToString(), false);
        }
    }
}