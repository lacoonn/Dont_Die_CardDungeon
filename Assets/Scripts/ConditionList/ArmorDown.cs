using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDown : ConditionBase
{
	public int unit;

	public ArmorDown(ApplicationTarget _applicationTarget, int _leftTurn, int _unit)
	{
		applicationTarget = _applicationTarget;
		applicationTime = ApplicationTime.Always;
		leftTurn = _leftTurn;
		unit = _unit;
	}

	override public void ApplyCondition(MonsterBase monsterBase)
	{
		monsterBase.currentArmor -= unit;
		if (monsterBase.currentArmor < 0)
			monsterBase.currentArmor = 0;
		leftTurn--;
	}
}
