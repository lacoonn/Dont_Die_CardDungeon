using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKnight : MonsterBase
{
	int stack = 0;

	private new void Awake()
	{
		baseMaxHp = 1500;
		baseAttackPoint = 360; // 턴당 120
		baseArmor = 20;
		attackTurnInterval = 3;
		monsterName = "해골 기사";
		description = "공격력 " + baseAttackPoint + "\n숙련된 기사의 해골입니다. 카드가 공격할 때마다 20% 확률로 반격합니다.";
	}

	public override IEnumerator AttackPlayer()
	{
		Debug.Log(monsterName + " attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

		Vector3 tempVector = transform.position;
		tempVector.z -= (float)0.1;

		yield return new WaitForSeconds(0.5f);

		turnLeftUntilAttack = attackTurnInterval;

		// override part
		currentAttackPoint = (int)(currentAttackPoint * (1 + 0.1 * stack)); // 공격력에 방어 스택을 적용
		stack = 0; // 방어 스택을 초기화
		// override part---

		BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
	}

	public override void Attacked(int damage)
	{
		int realDamage = (damage - currentArmor);
		if (realDamage < 0)
			realDamage = 0;

		// override part
		int randomResult = Random.Range(1, 101);
		if (randomResult <= 20) // 방어에 성공
		{
			Vector3 tempVector = transform.position;
			tempVector.z -= (float)0.1;
			Instantiate(monsterSkillEffect, tempVector, Quaternion.identity);

			stack++;
		}
		else // 방어에 실패
		{
			currentHp -= realDamage;
		}
		// override part end

		if (currentHp <= 0)
		{
			currentHp = 0;
		}
	}
}