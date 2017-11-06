using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezed : ConditionBase {

    override public void ApplyCondition(MonsterBase monsterBase)
    {
		monsterBase.canWork = false;
    }
}
