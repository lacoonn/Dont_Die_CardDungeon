using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionBase
{
    public enum ApplicationTarget { Player, Monster };
    public enum ApplicationTime { Always, TurnEnd };

    public ApplicationTarget applicationTarget;
    public ApplicationTime applicationTime;
    public int leftTurn;

    virtual public void ApplyCondition(BattleManager battleManager)
    {

    }

    virtual public void ApplyCondition(MonsterBase monsterBase)
	{
		
	}
}
