using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

    public class CharacterControl : MonoBehaviour
    {

        public float Speed;
        public Animator animator;
        public Material material;
        public enum TransitionParameter {
            Move,
        }

        // Since this is 2.5d we are going to mainly playing around "y" axis. 0 means we are going forward --> 180 means we are going backwards <--
       
      

        public void ChangeMaterial()
        {

            if (material == null)
            {
                Debug.LogError("No material specified");
            }

            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arrMaterials)
            {
                if (r.gameObject != this.gameObject)
                {
                    r.material = material;
                }
                
            }
        }

    }
}