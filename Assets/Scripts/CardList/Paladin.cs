using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : CardBase
{
	private new void Awake()
	{
		cardName = "성기사";
		description = "필드의 모든 카드들의 회복력을 공격력으로 전환한다.";
		// 합계 30
		baseAttackPoint = 16;
		baseHealPoint = 2;
		baseHp = 120;
	}

	public override void ApplyLeaderEffect()
	{
		// 필드의 모든 카드들의 회복력을 공격력으로 전환한다.
		foreach(GameObject cardItem in BattleManager.instance.fieldCards)
		{
			CardBase cardScript = cardItem.GetComponent<CardBase>();
			cardScript.attackPoint += cardScript.healPoint;
			cardScript.healPoint = 0;
		}
	}
}