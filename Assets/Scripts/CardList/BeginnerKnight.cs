using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerKnight : CardBase {
    new private void Awake()
    {
        // 합계 20
        baseAttackPoint = 10;
        baseHealPoint = 0;
        baseHp = 100;
    }

    override public void ApplyLeaderEffect()
	{
        attackPoint = (int)(attackPoint * 1.2);
        healPoint *= (int)(healPoint * 1.2);
    }
}