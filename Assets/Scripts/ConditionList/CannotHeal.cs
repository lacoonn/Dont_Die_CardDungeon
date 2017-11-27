using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotHeal : ConditionBase
{
	public CannotHeal(int _leftTurn)
	{
		applicationTarget = ApplicationTarget.Player;
		applicationTime = ApplicationTime.Always;
		leftTurn = _leftTurn;
	}

	public override void ApplyCondition(BattleManager battleManager)
	{
		Vector3 location = BattleManager.instance.fieldCards[1].transform.position;
		location.y += 1;
		location.z = (int)(location.z - 0.1);
		BattleManager.instance.CreateSkillText("치유 불가", location);

		foreach (GameObject cardItem in BattleManager.instance.fieldCards)
		{
			CardBase cardBase = cardItem.GetComponent<CardBase>();
			cardBase.healPoint = 0;
		}
		foreach(GameObject cardItem in BattleManager.instance.handCards)
		{
			CardBase cardBase = cardItem.GetComponent<CardBase>();
			cardBase.healPoint = 0;
		}
		leftTurn--;
	}
}