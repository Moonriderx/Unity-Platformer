using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moonrider
{
    [CreateAssetMenu(fileName = "New State", menuName = "Moonrider/AbilityData/Attack")]
    public class Attack : StateData
    {

		public float StartAttackTime;
		public float EndAttackTime;
		public List<string> ColliderNames = new List<string>();
		public bool MustCollide;
		public bool MustFaceAttacker;
		public float LethalRange;
		public int MaxHits;
		public List<RuntimeAnimatorController> DeathAnimators = new List<RuntimeAnimatorController>();

		public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), false);

			GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject; // instantiate the prefab
			AttackInfo info = obj.GetComponent<AttackInfo>();

			info.ResetInfo(this, characterState.GetCharacterControl(animator));

			if (!AttackManager.Instance.CurrentAttacks.Contains(info)) // if that info is not included in attackManager
			{
				AttackManager.Instance.CurrentAttacks.Add(info);
			}
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
			RegisterAttack(characterState, animator, stateInfo);
			DeregisterAttack(characterState, animator, stateInfo); 
        }

		public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
		{
			if (StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
			{

				foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
				{
					if (info == null)
					{
						continue;
					}
					if (!info.isRegistered && info.AttackAbility == this)
					{
						info.Register(this);
					}
				}
			}
		}

		public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
		{
			if (stateInfo.normalizedTime >= EndAttackTime) // if the animation time have passed the attackEndTime
			{
				foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
				{
					if (info == null)
					{
						continue;
					}

					if (info.AttackAbility == this && !info.isFinished)
					{
						info.isFinished = true;
						Destroy(info.gameObject);
					}
				} 
			}
		}
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
			ClearAttack();
        }

		public void ClearAttack()
		{
			for (int i = 0; i < AttackManager.Instance.CurrentAttacks.Count; i++)
			{
				if (AttackManager.Instance.CurrentAttacks[i] == null | AttackManager.Instance.CurrentAttacks[i].isFinished)
				{
					AttackManager.Instance.CurrentAttacks.RemoveAt(i);
				}
			}
		}

		public RuntimeAnimatorController GetDeathAnimator()
		{
			int index = Random.Range(0, DeathAnimators.Count);
			return DeathAnimators[index];
		}
    }
}
