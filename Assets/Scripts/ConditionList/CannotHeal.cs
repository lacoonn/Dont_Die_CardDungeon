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
		foreach(GameObject cardItem in BattleManager.instance.fieldCards)
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