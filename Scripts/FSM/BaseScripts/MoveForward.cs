using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public float Speed;


        public override void UpdateAbility(CharacterState characterStateBase, Animator animator)
        {
            CharacterControl control = characterStateBase.GetCharacterControl(animator);

            if (control.moveLeft && control.moveRight)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false); // if the character is not moving
                return; // this fix the "bug" where if u press A and D at the same time the character rotation "glitch". If u press the both buttons the character will just stops
            }

            if (!control.moveLeft && !control.moveRight) // if not pressing anything
            {
                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
                return;

            }

            if (control.moveRight)
            {
                //this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime); // the script is attached to this game object -> will move forward. Time.deltaTime will be used to negate the
                // time difference between each frame
                // However, it will not work here, because this script has no access to the game object. Instead use this ->


                control.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Character turns (rotates) toward the running direction

            }

            if (VirtualInputManager.Instance.moveLeft)
            {
                control.transform.Translate(Vector3.forward * Speed * Time.deltaTime); // the script is attached to this game object -> will move backwards.
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Character turns (rotates) toward the running direction

            }
        }
    }
}