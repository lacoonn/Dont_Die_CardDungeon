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
		Vector3 location = BattleManager.instance.monsterPos.position;
		location.x += 1;
		location.y += 1;
		location.z = (int)(location.z - 0.1);
		BattleManager.instance.CreateSkillText("방어력 감소 " + unit, location);
		
		monsterBase.currentArmor -= unit;
		if (monsterBase.currentArmor < 0)
			monsterBase.currentArmor = 0;
		leftTurn--;
	}
}
