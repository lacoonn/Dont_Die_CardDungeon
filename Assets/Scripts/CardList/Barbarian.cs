﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : CardBase {
    new private void Awake()
    {
        // 합계 40
        baseAttackPoint = 20;
        baseHealPoint = 0;
        baseHp = 200;
    }

    override public void ApplyLeaderEffect()
    {
        attackPoint = (int)(attackPoint * 1.5);
        healPoint *= (int)(healPoint * 1.5);
    }
}
