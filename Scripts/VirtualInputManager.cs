using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
    public class VirtualInputManager : MonoBehaviour
    {
        public static VirtualInputManager Instance = null; // We are going to turn this a singleton behavior
        // We only want a single instance of this virtual input manager
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        public bool moveRight;
        public bool moveLeft;
    }
}
