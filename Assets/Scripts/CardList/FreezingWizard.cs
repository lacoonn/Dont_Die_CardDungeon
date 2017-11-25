using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingWizard : CardBase
{
	public int baseFreezeChance = 4;
	public int currentFreezeChance;

	private new void Awake()
    {
		// 합계 30
		baseAttackPoint = 15;
        baseHealPoint = 5;
        baseHp = 100;

		currentFreezeChance = baseFreezeChance;

		cardName = "얼음 마법사";
		description = currentFreezeChance + "% 확률로 적을 1턴간 얼린다.";
	}

	public override void UpdateSkillChance()
	{
		if (status == Status.inField)
		{
			currentFreezeChance = BattleManager.instance.combination * baseFreezeChance;
		}
		else
		{
			currentFreezeChance = baseFreezeChance;
		}
		description = currentFreezeChance + "% 확률로 적을 1턴간 얼린다.";
	}

	public override void ApplyLeaderEffect()
	{
		int randomFreeze = Random.Range(1, 101); // 치명타 공격 확률
		if (1 <= randomFreeze && randomFreeze <= 20) // 연속 공격 성공
		{
			BattleManager.instance.conditionList.Add(new Freezed(ConditionBase.ApplicationTarget.Monster, 1));
			Debug.Log(cardName + "가 연속 공격을 성공했습니다.");
		}
		else // 연속 공격 실패
		{
			Debug.Log(cardName + "가 연속 공격을 실패했습니다.");
		}
		
	}
}
