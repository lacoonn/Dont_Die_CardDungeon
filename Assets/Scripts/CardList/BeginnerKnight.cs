using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerKnight : CardBase {
    new private void Awake()
    {
		cardName = "초보기사";
		description = "이 카드의 능력치를 20%만큼 증가시킨다.";
		// 합계 20
		baseAttackPoint = 10;
        baseHealPoint = 0;
        baseHp = 100;
    }

    override public void ApplyLeaderEffect()
	{
        attackPoint = (int)(attackPoint * 1.2);
        healPoint = (int)(healPoint * 1.2);
    }
}