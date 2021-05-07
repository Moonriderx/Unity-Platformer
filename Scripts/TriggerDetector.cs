using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
	public class TriggerDetector : MonoBehaviour
	{

		private CharacterControl owner;

		private void Awake()
		{
			owner = this.GetComponentInParent<CharacterControl>();
		}
		private void OnTriggerEnter(Collider col)
		{
			if (owner.RagdollParts.Contains(col))
			{
				return;
			}

			CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

			if (attacker == null) // it means that it is physical object, not the player itself
			{
				return;
			}

			if (col.gameObject == attacker.gameObject)
			{
				return;
			}

			// after passing all this checks, we know that the collider is a body part from another player

			if (!owner.CollidingParts.Contains(col))
			{
				owner.CollidingParts.Add(col);
			}
		}
		private void OnTriggerExit(Collider attacker)
		{
			if (owner.CollidingParts.Contains(attacker))
			{
				owner.CollidingParts.Remove(attacker);
			}
		}

	}

}

