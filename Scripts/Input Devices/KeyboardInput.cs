using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{


    public class KeyboardInput : MonoBehaviour
    {
         
        void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.moveRight = true;
            }
            else
            {
                VirtualInputManager.Instance.moveRight = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.moveLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.moveLeft = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.Jump = true;
            }
            else
            {
                VirtualInputManager.Instance.Jump = false;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                VirtualInputManager.Instance.Attack = true;
            }
            else
            {
                VirtualInputManager.Instance.Attack = false;

            }
        }
    }
}