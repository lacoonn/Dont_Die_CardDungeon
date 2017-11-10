using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPriest : CardBase
{
    new private void Awake()
    {
		cardName = "회복의 요정";
		description = "이 카드의 회복력을 50%만큼 증가시킨다.";
		// 합계 30
		baseAttackPoint = 5;
        baseHealPoint = 20;
        baseHp = 50;
    }

	public override void ApplyLeaderEffect()
	{
		foreach (GameObject gameObject in BattleManager.instance.fieldCards)
		{
			// 필드의 모든 카드의 회복력을 1.5배 증가시킨다.
			CardBase cardBase = gameObject.GetComponent<CardBase>();
			cardBase.healPoint = (int)(cardBase.healPoint * 1.5);
		}
	}
}
