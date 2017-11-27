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
		description = "공격력 " + baseAttackPoint + "\n조금 예리한 시선을 가진 고블린입니다. 50% 확률로 1.5배의 피해를 줍니다. 20% 확률로 다음 턴에 한 번 더 공격합니다.";
	}

	public override IEnumerator AttackPlayer()
	{
		Debug.Log("GoblinAssassin attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

		int randomCritical = Random.Range(1, 101); // 치명타 공격 확률
		if (1 <= randomCritical && randomCritical <= 50) // 치명타 공격 성공
		{
			Debug.Log(monsterName + "가 치명타 공격을 성공했습니다.");
			CreateSkillEffect();
			CreateSkillText("치명타 공격");
			currentAttackPoint = (int)(currentAttackPoint * 1.5);
		}
		else // 치명타 공격 실패
		{
			Debug.Log(monsterName + "가 치명타 공격을 실패했습니다.");
		}

		int randomContinuous = Random.Range(1, 101); // 치명타 공격 확률
		bool continuousAttack = false;
		if (1 <= randomContinuous && randomContinuous <= 20) // 연속 공격 성공
		{
			Debug.Log(monsterName + "가 연속 공격을 성공했습니다.");
			CreateSkillEffect();
			CreateSkillText("연속 공격");
			continuousAttack = true;
		}
		else // 연속 공격 실패
		{
			Debug.Log(monsterName + "가 연속 공격을 실패했습니다.");
		}

		yield return new WaitForSeconds(0.5f);
		if (continuousAttack) // 연속 공격에 성공하면 다음 턴도 공격
			turnLeftUntilAttack = 1;
		else
			turnLeftUntilAttack = attackTurnInterval;
		BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
	}
}