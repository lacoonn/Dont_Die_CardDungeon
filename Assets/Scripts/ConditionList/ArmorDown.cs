using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDown : ConditionBase
{
	public ArmorDown(ApplicationTarget _applicationTarget, int _leftTurn)
	{
		applicationTarget = _applicationTarget;
		applicationTime = ApplicationTime.Always;
		leftTurn = _leftTurn;
	}

	override public void ApplyCondition(MonsterBase monsterBase)
	{
		int armorDownValue = 30;
		monsterBase.currentArmor -= armorDownValue;
		if (monsterBase.currentArmor < 0)
			monsterBase.currentArmor = 0;
		leftTurn--;
	}
}
