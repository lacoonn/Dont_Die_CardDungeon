using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBreaker : CardBase
{
	public int baseDecreaseUnit = 10;
	public int currentDecreaseUnit;

	new private void Awake()
    {
        // 합계 30
        baseAttackPoint = 17;
        baseHealPoint = 0;
        baseHp = 130;

		currentDecreaseUnit = baseDecreaseUnit;

		cardName = "드워프 전사";
		description = "적의 방어력을 " + currentDecreaseUnit + "만큼 감소시킨다.";
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
		description = "적의 방어력을 " + currentDecreaseUnit + "만큼 감소시킨다.";
	}

	public override void ApplyLeaderEffect()
	{
		BattleManager.instance.conditionList.Add(new ArmorDown(ConditionBase.ApplicationTarget.Monster, 2, currentDecreaseUnit));
	}
}
