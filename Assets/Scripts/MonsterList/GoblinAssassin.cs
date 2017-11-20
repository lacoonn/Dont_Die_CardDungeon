using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAssassin : MonsterBase
{
	new private void Awake()
	{
		baseMaxHp = 500;
		baseAttackPoint = 100; // 턴당 100
		baseArmor = 0;
		attackTurnInterval = 1;
		monsterName = "고블린";
		description = "공격력 " + baseAttackPoint + "\n조금 예리한 시선을 가진 고블린입니다. 50% 확률로 1.5배의 피해를 줍니다.";
	}

	public override IEnumerator AttackPlayer()
	{
		Debug.Log("GoblinAssassin attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

		Vector3 tempVector = transform.position;
		tempVector.z -= (float)0.1;

		// override part
		int randomResult = Random.Range(1, 101);
		if (1 <= randomResult && randomResult <= 50) // 치명타 공격 성공
		{
			Instantiate(monsterSkillEffect, tempVector, Quaternion.identity);
			currentAttackPoint = (int)(currentAttackPoint * 1.5);
		}
		else // 치명타 공격 실패
		{
		}
		// override part end

		yield return new WaitForSeconds(0.5f);
		turnLeftUntilAttack = attackTurnInterval;
		BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
	}
}