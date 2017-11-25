using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSoldier : MonsterBase
{
	bool oneMoreAttack = true;

	private new void Awake()
	{
		baseMaxHp = 500;
		baseAttackPoint = 200; // 턴당 100
		baseArmor = 0;
		attackTurnInterval = 2;
		monsterName = "해골 병사";
		description = "공격력 " + baseAttackPoint + "\n팔이 하나 더 있는 해골 병사입니다. 2턴 연속으로 공격합니다.";
	}

	public override IEnumerator AttackPlayer()
	{
		Debug.Log("SkeletonSoldier 1 attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

		Vector3 tempVector = transform.position;
		tempVector.z -= (float)0.1;

		yield return new WaitForSeconds(0.5f);

		// override part
		if (oneMoreAttack)
		{
			turnLeftUntilAttack = 1;
			oneMoreAttack = false;
		}
		else
		{
			turnLeftUntilAttack = attackTurnInterval;
			oneMoreAttack = true;
		}
		// override part end

		BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
	}
}