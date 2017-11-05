using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWizard : CardBase
{
    new private void Awake()
    {
        // 합계 30
        baseAttackPoint = 20;
        baseHealPoint = 0;
        baseHp = 100;
    }
}
