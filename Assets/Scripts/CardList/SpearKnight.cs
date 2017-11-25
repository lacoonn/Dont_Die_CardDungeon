using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearKnight : CardBase
{
	private new void Awake()
	{
		cardName = "창기사";
		description = "필드의 다른 카드의 공격력을 80%로 흡수해서 공격한다.";
		// 합계 30
		baseAttackPoint = 24;
		baseHealPoint = 0;
		baseHp = 60;
	}

	public override void ApplyLeaderEffect()
	{
		// 필드의 다른 카드의 공격력을 80%로 흡수해서 공격한다.
		CardBase cardScript0 = BattleManager.instance.fieldCards[0].GetComponent<CardBase>();
		CardBase cardScript2 = BattleManager.instance.fieldCards[2].GetComponent<CardBase>();
		CardBase spearKnightScript = BattleManager.instance.fieldCards[1].GetComponent<CardBase>();

		spearKnightScript.attackPoint += (cardScript0.attackPoint + cardScript2.attackPoint);
		cardScript0.attackPoint = 0;
		cardScript2.attackPoint = 0;


		//BattleManager.instance.conditionList.Add(new Freezed(ConditionBase.ApplicationTarget.Monster, 1));
	}
}