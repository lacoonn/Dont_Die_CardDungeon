using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncouragementPriest : CardBase
{
    new private void Awake()
    {
		cardName = "응원의 요정";
		description = "필드의 모든 카드의 공격력을 1.2배 증가시킨다.";
		// 합계 30
		baseAttackPoint = 16;
        baseHealPoint = 10;
        baseHp = 40;
    }

	public override void ApplyLeaderEffect()
	{
		foreach (GameObject gameObject in BattleManager.instance.fieldCards)
		{
			// 필드의 모든 카드의 공격력을 1.2배 증가시킨다.
			CardBase cardBase = gameObject.GetComponent<CardBase>();
			cardBase.attackPoint = (int)(cardBase.attackPoint * 1.2);
		}
	}
}
