using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
	public class AttackInfo : MonoBehaviour
	{

		public CharacterControl Attacker = null;
		public Attack AttackAbility;
		public List<string> ColliderNames = new List<string>();

		public bool MustCollide;
		public bool MustFaceAttacker;
		public float LethalRange;
		public int MaxHits;
		public int CurrentHints;
		public bool isRegistered;
		public bool isFinished;

		public void ResetInfo(Attack attack, CharacterControl attacker)
		{
			isRegistered = false; // when attack first beggins it won't be registered
			isFinished = false;
			AttackAbility = attack;
			Attacker = attacker;

		}


		public void Register(Attack attack)
		{
			isRegistered = true;
			

			AttackAbility = attack;
			ColliderNames = attack.ColliderNames;
			MustCollide = attack.MustCollide;
			MustFaceAttacker = attack.MustFaceAttacker;
			LethalRange = attack.LethalRange;
			MaxHits = attack.MaxHits;
			CurrentHints = 0;
		}
	}

}
