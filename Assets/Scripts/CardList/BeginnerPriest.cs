using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerPriest : CardBase
{
    new private void Awake()
    {
		cardName = "초보사제";
		description = "이 카드의 능력치를 20%만큼 증가시킨다.";
		// 합계 20
		baseAttackPoint = 5;
        baseHealPoint = 10;
        baseHp = 50;
    }

    public override void ApplyLeaderEffect()
    {
        attackPoint = (int)(attackPoint * 1.2);
        healPoint = (int)(healPoint * 1.2);
    }
}
