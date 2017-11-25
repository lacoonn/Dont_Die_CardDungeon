using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sage : CardBase
{
	public int baseDecreaseUnit = 30;
	public int currentDecreaseUnit;
	private new void Awake()
	{
		// 합계 30
		baseAttackPoint = 7;
		baseHealPoint = 13;
		baseHp = 100;

		currentDecreaseUnit = baseDecreaseUnit;

		cardName = "현자";
		description = "몬스터의 공격력을 2턴 동안 " + currentDecreaseUnit + " 만큼 감소시킨다.";
	}

	public override void UpdateSkillChance()
	{
		if (status == Status.inField)
		{
			currentDecreaseUnit = BattleManager.instance.combination * baseDecreaseUnit;
		}
		else
		{
			currentDecreaseUnit = baseDecreaseUnit;
		}
		description = "몬스터의 공격력을 2턴 동안 " + currentDecreaseUnit + " 만큼 감소시킨다.";
	}

	public override void ApplyLeaderEffect()
	{
		BattleManager.instance.conditionList.Add(new DecreaseAttack(3, currentDecreaseUnit));
	}
}