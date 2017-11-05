using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBreaker : CardBase {
    private void Awake()
    {
        // 합계 30
        baseAttackPoint = 17;
        baseHealPoint = 0;
        baseHp = 130;
    }
}
