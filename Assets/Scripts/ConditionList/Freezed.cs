using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezed : ConditionBase
{
	public Freezed(ApplicationTarget _applicationTarget, int _leftTurn)
	{
		applicationTarget = _applicationTarget;
		applicationTime = ApplicationTime.Always;
		leftTurn = _leftTurn;
	}

	override public void ApplyCondition(MonsterBase monsterBase)
    {
		monsterBase.canWork = false;
		leftTurn--;
	}
}
