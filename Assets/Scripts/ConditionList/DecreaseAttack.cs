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
		Vector3 location = BattleManager.instance.monsterPos.position;
		location.x += 1;
		location.y += 1;
		location.z = (int)(location.z - 0.1);
		BattleManager.instance.CreateSkillText("공격력 감소 " + decreasedAttackPoint, location);

		monsterBase.currentAttackPoint -= decreasedAttackPoint;
		if (monsterBase.currentAttackPoint < 0)
			monsterBase.currentAttackPoint = 0;
		leftTurn--;
	}
}