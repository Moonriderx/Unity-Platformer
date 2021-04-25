using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public float Speed;
        public AnimationCurve SpeedGraph;
        public float BlockDistance;
        private bool Self;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterStateBase.GetCharacterControl(animator);

            if (control.Jump)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
            }

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

                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Character turns (rotates) toward the running direction
                //this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime); // the script is attached to this game object -> will move forward. Time.deltaTime will be used to negate the
                // time difference between each frame
                // However, it will not work here, because this script has no access to the game object. Instead use this ->
                if (!CheckFront(control)) // we only wanna move when CheckFront is false -> when there is nothing in front
                {
                    control.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                    
                }

             

            }

            if (control.moveLeft) // if we inherit from VirtualInputManager, when we add NPCs they will move too, so we inherit from CharacterControl script
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Character turns (rotates) toward the running direction
                if (!CheckFront(control))
                {
                    control.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime); // the script is attached to this game object -> will move backwards.
                    

                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        bool CheckFront(CharacterControl control)
        {
            
            foreach (GameObject o in control.FrontSpheres) // for each of the spheres in the list, do a raycast 
            {
                Self = false;
                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance)) // we shoot the ray from our spheres in front
                {
                    foreach(Collider c in control.RagdollParts) // whenever the raycast hit an object, we check if the object belongs to the player
                    {
                       if (c.gameObject == hit.collider.gameObject) // if it is the same gameobject as what the raycast is hitting
                        {
                            Self = true; // if the raycast is hitting the player itself
                            break;
                        }
                    }
                    if (!Self)
                    {
                        return true;
                    }

          
                }
            }
            return false;
        }
    }
}