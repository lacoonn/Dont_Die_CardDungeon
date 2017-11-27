using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinShaman : MonsterBase {
	new private void Awake()
	{
		baseMaxHp = 2000;
		baseAttackPoint = 200; // 턴당 100
		baseArmor = 0;
		attackTurnInterval = 2;
		monsterName = "고블린";
		description = "공격력 " + baseAttackPoint + "\n도박을 좋아하는 고블린 주술사입니다. 공격 시 50% 확률로 체력을 30% 회복하거나 나머지 50% 확률로 피해를 50% 더 줍니다.";
	}

	public override IEnumerator AttackPlayer()
	{
		Debug.Log("GoblinAssassin attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

		// override part
		int randomResult = Random.Range(1, 101);
		if (1 <= randomResult && randomResult <= 50) // 피해 증가 스킬
		{
			CreateSkillEffect();
			CreateSkillText("공격력 증가");
			yield return new WaitForSeconds(0.5f);
			currentAttackPoint = (int)(currentAttackPoint * 1.5);
		}
		else // 체력 회복 스킬
		{
			CreateSkillEffect();
			CreateSkillText("체력 회복");
			yield return new WaitForSeconds(0.5f);
			HealingSkill();
		}
		// override part end

		turnLeftUntilAttack = attackTurnInterval;
		BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
	}

	public void HealingSkill()
	{
		currentHp += (int)(maxHp * 0.3);
		if (currentHp > maxHp)
			currentHp = maxHp;
	}
}