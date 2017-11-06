using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWizard : CardBase
{
    new private void Awake()
    {
		cardName = "독 마법사";
		description = "적에게 3턴 동안 매 턴당 공격력의 30%의 데미지를 주는 독 마법을 건다.";
		// 합계 30
		baseAttackPoint = 20;
        baseHealPoint = 0;
        baseHp = 100;
    }

	public override void ApplyLeaderEffect()
	{
		Battle.instance.conditionList.Add(new Poison(ConditionBase.ApplicationTarget.Monster, 3, (int)(attackPoint * 0.3)));
	}
}
