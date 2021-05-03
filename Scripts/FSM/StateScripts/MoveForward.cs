using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public bool Constant;
        public float Speed;
        public AnimationCurve SpeedGraph;
        public float BlockDistance;

        

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

            if (Constant) // if we toggled constant bool in our scriptable object "LeadJab_MoveForward, we are going to move in a certain way (by pressing enter)
            {
                ConstantMove(control, animator, stateInfo);
            }
            else
            {
                ControlledMove(control, animator, stateInfo); // move with the keyboard
            }

            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(control))
            {
				control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                
            }
        }

        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
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
					control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));

				}



            }

            if (control.moveLeft) // if we inherit from VirtualInputManager, when we add NPCs they will move too, so we inherit from CharacterControl script
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Character turns (rotates) toward the running direction
                if (!CheckFront(control))
                {
					control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));


				}
            }
        }

        bool CheckFront(CharacterControl control)
        {
            
            foreach (GameObject o in control.FrontSpheres) // for each of the spheres in the list, do a raycast 
            {
                
                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance)) // we shoot the ray from our spheres in front
                {
                    if (!control.RagdollParts.Contains(hit.collider)) // if the list does not contain the collider
                    {
                        if (!IsBodyPart(hit.collider)) // even if the front raycast hits something infront, we wanna make sure that is not a body part
                        {
                            return true;
                        }
                        
                    }
                                             
                }
            }
            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>(); // look at the character control that is attached to the collider ( the root of the collider )
            if (control == null) // that means that is not a body part
            {
                return false;
            }

            if (control.gameObject == col.gameObject) // if the collider is character control itself -> Thats mean it is not a body part. It is the root of the character
            {
                return false;
            }

            if (control.RagdollParts.Contains(col)) // make sure that it is not in the list of the ragdol parts
            {
                return true;
            }
            return false;
        }
    }
}
