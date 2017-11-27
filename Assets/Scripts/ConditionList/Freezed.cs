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
		Vector3 location = BattleManager.instance.monsterPos.position;
		location.x += 1;
		location.y += 1;
		location.z = (int)(location.z - 0.1);
		BattleManager.instance.CreateSkillText("행동 불가", location);

		monsterBase.canWork = false;
		leftTurn--;
	}
}
