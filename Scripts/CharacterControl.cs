using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{

	public class CharacterControl : MonoBehaviour
	{

		public Animator SkinnedMeshAnimator;
		public Material material;
		public bool moveRight;
		public bool moveLeft;
		public bool Jump;
		public bool Attack;
		public GameObject ColliderEdgePrefab;
		public List<GameObject> BottomSpheres = new List<GameObject>(); // The list will hold the game objects bottomsphere. We will use them for Ground Detection
		public List<GameObject> FrontSpheres = new List<GameObject>(); // Front Spheres info list
		public List<Collider> RagdollParts = new List<Collider>(); // we put all the ragdoll parts into the list
		public List<Collider> CollidingParts = new List<Collider>(); // store all the character body parts that comes in contact

		public float GravityMultiplier;
		public float PullMultiplier;

		private Rigidbody rigid;
		public Rigidbody RIGID_BODY


		{
			get
			{
				if (rigid == null)
				{
					rigid = GetComponent<Rigidbody>();
				}
				return rigid;
			}
		}
		public enum TransitionParameter
		{
			Move, Jump, ForceTransition, Grounded, Attack
		}

		// Since this is 2.5d we are going to mainly playing around "y" axis. 0 means we are going forward --> 180 means we are going backwards <--


		private void Awake() // as soon as the game starts look for the box collider on the character controller
		{
			bool SwitchBack = false;
			if (!IsFacingForward())
			{
				SwitchBack = true;
			}
			FaceForward(true);
			SetRagdollParts();
			SetColliderSphere();

			if (SwitchBack)
			{
				FaceForward(false);
			}
		}

		/*private IEnumerator Start() // Only for testing purposes
        {
            yield return new WaitForSeconds(5f);
            RIGID_BODY.AddForce(200f * Vector3.up);
            yield return new WaitForSeconds(0.5f);
            TurnOnRagdoll();
        }*/

		
		

        private void SetRagdollParts()
        {
            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>(); // will give us all the colliders in the hierarchy of the game object

            foreach(Collider c in colliders)
            {
                if (c.gameObject != this.gameObject) // if the collider we found is not the same as what is in the character control
                {
                    c.isTrigger = true; // it means the collider will not be a physical object anymore (we turn the collider into a trigger)
                    RagdollParts.Add(c); // add the parts in to the list
					c.gameObject.AddComponent<TriggerDetector>(); // for each of the body parts add the trigger component
                }
            }

        }

        public void TurnOnRagdoll()
        {
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;

            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SkinnedMeshAnimator.enabled = false;
            SkinnedMeshAnimator.avatar = null;

            foreach(Collider c in RagdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }



        private void SetColliderSphere()
        {
            BoxCollider box = GetComponent<BoxCollider>(); // find the collider attached to player and find the four sides of it.

            float bottom = box.bounds.center.y - box.bounds.extents.y; // bottom position of the collider (head)
            float top = box.bounds.center.y + box.bounds.extents.y; // top position of the collider (feet)
            float front = box.bounds.center.z + box.bounds.extents.z; // front position (torso front)
            float back = box.bounds.center.z - box.bounds.extents.z; // back position (torso back)

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

            bottomFront.transform.parent = this.transform; // spawn the instanciated game objects as the chield to the player
            bottomBack.transform.parent = this.transform;
            topFront.transform.parent = this.transform;

            BottomSpheres.Add(bottomFront); // add them to the list
            BottomSpheres.Add(bottomBack);
            FrontSpheres.Add(bottomFront);
            FrontSpheres.Add(topFront);

            //float sec = (bottomFront.transform.position - bottomBack.transform.position).magnitude; // the vector in between spheres for one section
            float horSec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f; // the length for single section of all 5 sections (horizontal)
            CreateMiddleSpheres(bottomFront, -this.transform.forward, horSec, 4, BottomSpheres);
            float verSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f; // the length for single section of all 10 sections (vertical)
            CreateMiddleSpheres(bottomFront, this.transform.up, verSec, 9, FrontSpheres);

        }

        private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f) // that means the player is falling

            {
                RIGID_BODY.velocity += (-Vector3.up * GravityMultiplier); // Make animationCurve in jump script for better control
            }

            if (RIGID_BODY.velocity.y > 0f & !Jump )// if the player is going up and if we release the jump button
            {
                RIGID_BODY.velocity += (-Vector3.up * PullMultiplier);
            }
        }

        public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec, int iterations, List<GameObject> spheresList)
        {
            for (int i = 0; i < iterations; i++)
            {
                Vector3 pos = start.transform.position + (dir * sec * (i + 1)); // each of the position is going to be starting from the back. bottomBack * 1, bottomBack * 2, bottomBack * 3 etc....

                GameObject newObj = CreateEdgeSphere(pos);
                newObj.transform.parent = this.transform;
                spheresList.Add(newObj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos) // it takes the position info
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity); // Instanciate it to the position with 0 rotation
            return obj;
        }


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

		public void MoveForward(float Speed, float SpeedGraph)
		{
			transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);

		}

		public void FaceForward(bool forward)
		{
			if (forward)
			{
				transform.rotation = Quaternion.Euler(0f, 0f, 0f); // we wanna character to turn forward
			}
			else
			{
				transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			}
		}

		public bool IsFacingForward()
		{
			if (transform.forward.z > 0f) // that mean the character is facing the correct side
			{
				return true;
			}
			else
			{
				return false;
			}

		}



	}
}
