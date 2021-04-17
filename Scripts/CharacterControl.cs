using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    public class CharacterControl : MonoBehaviour
    {

        public float Speed;
        public Animator animator;

        public enum TransitionParameter {
            Move,
        }

        // Since this is 2.5d we are going to mainly playing around "y" axis. 0 means we are going forward --> 180 means we are going backwards <--
       
        void Update()
        {

            if (VirtualInputManager.Instance.moveLeft && VirtualInputManager.Instance.moveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false); // if the character is not moving
                return; // this fix the "bug" where if u press A and D at the same time the character rotation "glitch". If u press the both buttons the character will just stops
            }

            if (!VirtualInputManager.Instance.moveLeft && !VirtualInputManager.Instance.moveRight) // if not pressing anything
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
            }

                if (VirtualInputManager.Instance.moveRight)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime); // the script is attached to this game object -> will move forward. Time.deltaTime will be used to negate the
                                                                                             // time difference between each frame
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Character turns (rotates) toward the running direction
                animator.SetBool(TransitionParameter.Move.ToString(), true); // if the character is moving

            }

            if (VirtualInputManager.Instance.moveLeft)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime); // the script is attached to this game object -> will move backwards.
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Character turns (rotates) toward the running direction
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }
    }
}