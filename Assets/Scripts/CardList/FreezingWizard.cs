using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingWizard : CardBase
{
    new private void Awake()
    {
		cardName = "얼음 마법사";
		description = "n% 확률로 적을 1턴간 얼린다.";
		// 합계 30
		baseAttackPoint = 15;
        baseHealPoint = 5;
        baseHp = 100;
    }

	public override void ApplyLeaderEffect()
	{
		BattleManager.instance.conditionList.Add(new Freezed(ConditionBase.ApplicationTarget.Monster, 1));
	}
}
