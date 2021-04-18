using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        public float Distance;
        [Range(0.01f, 1f)]
        public float CheckTime;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
           
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator); // in every update we are going to look at in our CharacterControl

            if (stateInfo.normalizedTime >= CheckTime) // if animation has reached a certain point (in %) (if this is passed the checkTime =>
            {
                if (IsGrounded(control)) // if the character is grounded
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), true);
                }
                else
                {
                    animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), false);
                }
            }

            
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool IsGrounded(CharacterControl control)
        {
            if (control.RIGID_BODY.velocity.y > -0.01f && control.RIGID_BODY.velocity.y <= 0f) // if the character velocity is 0, that means the character does not fall (he has hit the ground)
                // Since velocity is float value, it would not be exactly 0 most of the times
            {
                return true;
            }
            foreach(GameObject o in control.BottomSpheres) // for each of the spheres, do a raycast 
            {
                Debug.DrawRay(o.transform.position, -Vector3.up * 0.7f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, Distance)) // we shoot the ray from our spheres down
                {
                    return true;
                }
            }
            return false;
        }
    }
}