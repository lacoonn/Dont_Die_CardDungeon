﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonReaper : MonsterBase
{
	private new void Awake()
	{
		baseMaxHp = 2000;
		baseAttackPoint = 300; // 턴당 150
		baseArmor = 0;
		attackTurnInterval = 2;
		monsterName = "사신";
		description = "공격력 " + baseAttackPoint + "\n낫을 든 강력한 사신입니다. 50% 확률로 2배의 피해를 입힙니다. 30% 확률로 다음 턴 카드들의 회복을 무효화합니다.";
	}

	public override IEnumerator AttackPlayer()
	{
		Debug.Log(monsterName + " attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

		// override part
		int randomCannotHeal = Random.Range(1, 101);
		int randomCritical = Random.Range(1, 101);
		if (randomCannotHeal <= 30)
		{
			CreateSkillEffect();
			CreateSkillText("회복 무효");
			// 회복 무효 디버프
			BattleManager.instance.conditionList.Add(new CannotHeal(1));
		}
		if (randomCritical <= 50)
		{
			CreateSkillEffect();
			CreateSkillText("치명타 공격");
			currentAttackPoint = (currentAttackPoint * 2);
		}
		yield return new WaitForSeconds(0.5f);
		// override part end

		BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
	}
}