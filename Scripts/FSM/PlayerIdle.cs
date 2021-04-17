using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moonrider
{

    public class PlayerIdle : CharacterStateBase
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // When we call it on the first time player enters this animation
        {
            
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // For every single frame that player is on that animation
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

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // This part of the script will be called when the player Exit the animation
        {
            
        }

        
    }
}