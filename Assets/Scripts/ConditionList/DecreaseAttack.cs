using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseAttack : ConditionBase
{
	int decreasedAttackPoint;

	public DecreaseAttack(int _leftTurn, int _decreasedAttackPoint)
	{
		applicationTarget = ApplicationTarget.Monster;
		applicationTime = ApplicationTime.Always;
		leftTurn = _leftTurn;
		decreasedAttackPoint = _decreasedAttackPoint;
	}

	public override void ApplyCondition(MonsterBase monsterBase)
	{
		monsterBase.currentAttackPoint -= decreasedAttackPoint;
		if (monsterBase.currentAttackPoint < 0)
			monsterBase.currentAttackPoint = 0;
		leftTurn--;
	}
}
