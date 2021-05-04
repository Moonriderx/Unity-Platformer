using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
	public class AttackManager : Singleton<AttackManager>
	{
		public List<AttackInfo> CurrentAttacks = new List<AttackInfo>(); // List of all current attacks

	}
}

