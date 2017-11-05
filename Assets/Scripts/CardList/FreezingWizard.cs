using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingWizard : CardBase
{
    private void Awake()
    {
        // 합계 30
        baseAttackPoint = 15;
        baseHealPoint = 5;
        baseHp = 100;
    }
}
