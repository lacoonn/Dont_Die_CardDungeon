using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDown : ConditionBase {

	override public void ApplyCondition(MonsterBase monsterBase)
	{
		int armorDownValue = 30;
		monsterBase.currentArmor -= armorDownValue;
		if (monsterBase.currentArmor < 0)
			monsterBase.currentArmor = 0;
	}
}
