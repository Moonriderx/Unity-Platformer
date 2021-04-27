using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    public class ManualInput : MonoBehaviour
    {
        private CharacterControl characterControl; // Script have access to the characterControl script

        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }
        void Update()
        {
            if (VirtualInputManager.Instance.moveRight)
            {
                characterControl.moveRight = true;
            } 
            else
            {
                characterControl.moveRight = false;
            }

            if (VirtualInputManager.Instance.moveLeft)
            {
                characterControl.moveLeft = true;
            }
            else
            {
                characterControl.moveLeft = false;
            }

            if (VirtualInputManager.Instance.Jump)
            {
                characterControl.Jump = true;
            } 
            else
            {
                characterControl.Jump = false;
            }

            if (VirtualInputManager.Instance.Attack)
            {
                characterControl.Attack = true;
            }
            else
            {
                characterControl.Attack = false;
            }
        }
    }
}