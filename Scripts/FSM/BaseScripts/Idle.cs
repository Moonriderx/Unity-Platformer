using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/Idle")]
    public class Idle : StateData
    {
        public override void UpdateAbility(CharacterState characterStateBase, Animator animator)
        {
            if (VirtualInputManager.Instance.moveRight)
            {

                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true); // if the character is moving set the boolean to true

            }

            if (VirtualInputManager.Instance.moveLeft)
            {

                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
            }
        }
    }
}