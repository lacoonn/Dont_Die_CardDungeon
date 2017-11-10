using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBreaker : CardBase
{
    new private void Awake()
    {
		cardName = "망치전사";
		description = "저의 방어력을 30만큼 감소시킨다.";
        // 합계 30
        baseAttackPoint = 17;
        baseHealPoint = 0;
        baseHp = 130;
    }

	public override void ApplyLeaderEffect()
	{
		BattleManager.instance.conditionList.Add(new ArmorDown(ConditionBase.ApplicationTarget.Monster, 2));
	}
}
