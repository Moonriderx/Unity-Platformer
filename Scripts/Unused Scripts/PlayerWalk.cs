//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace Moonrider
//{

//    public class PlayerWalk : CharacterState
//    {
//        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
//        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // When we call it on the first time player enters this animation
//        {

//        }

//        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // For every single frame that player is on that animation
//        {
//            if (VirtualInputManager.Instance.moveLeft && VirtualInputManager.Instance.moveRight)
//            {
//                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false); // if the character is not moving
//                return; // this fix the "bug" where if u press A and D at the same time the character rotation "glitch". If u press the both buttons the character will just stops
//            }

//            if (!VirtualInputManager.Instance.moveLeft && !VirtualInputManager.Instance.moveRight) // if not pressing anything
//            {
//                animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
//                return;

//            }

//            if (VirtualInputManager.Instance.moveRight)
//            {
//                //this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime); // the script is attached to this game object -> will move forward. Time.deltaTime will be used to negate the
//                // time difference between each frame
//                // However, it will not work here, because this script has no access to the game object. Instead use this ->


//                GetCharacterControl(animator).transform.Translate(Vector3.forward * GetCharacterControl(animator).Speed * Time.deltaTime);
//                GetCharacterControl(animator).transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Character turns (rotates) toward the running direction
                
//            }

//            if (VirtualInputManager.Instance.moveLeft)
//            {
//                GetCharacterControl(animator).transform.Translate(Vector3.forward * GetCharacterControl(animator).Speed * Time.deltaTime); // the script is attached to this game object -> will move backwards.
//                GetCharacterControl(animator).transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Character turns (rotates) toward the running direction
                
//            }
//        }

//        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
//        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // This part of the script will be called when the player Exit the animation
//        {

//        }
//    }
//}