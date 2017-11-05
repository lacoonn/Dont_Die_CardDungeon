using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerWizard : CardBase
{
    new private void Awake()
    {
        // 합계 20
        baseAttackPoint = 10;
        baseHealPoint = 5;
        baseHp = 50;
    }

	public override void ApplyLeaderEffect()
	{
		attackPoint = (int)(attackPoint * 1.2);
		healPoint *= (int)(healPoint * 1.2);
	}
}