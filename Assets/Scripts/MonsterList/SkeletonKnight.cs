using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKnight : MonsterBase
{
	private new void Awake()
	{
		baseMaxHp = 500;
		baseAttackPoint = 100; // 턴당 100
		baseArmor = 0;
		attackTurnInterval = 2;
		monsterName = "해골 기사";
		description = "공격력 " + baseAttackPoint + "\n숙련된 기사의 해골입니다. 카드가 공격할 때마다 20% 확률로 반격합니다.";
	}

	public override void Attacked(int damage)
	{
		int realDamage = (damage - currentArmor);
		if (realDamage < 0)
			realDamage = 0;

		currentHp -= realDamage;

		// override part
		int randomResult = Random.Range(1, 101);
		if (randomResult <= 20)
		{
			Vector3 tempVector = transform.position;
			tempVector.z -= (float)0.1;

			Instantiate(monsterSkillEffect, tempVector, Quaternion.identity);

			StartCoroutine(AttackPlayer());
		}
		// override part end

		if (currentHp <= 0)
		{
			currentHp = 0;
			BattleManager.instance.EndBattle();
		}
	}
}