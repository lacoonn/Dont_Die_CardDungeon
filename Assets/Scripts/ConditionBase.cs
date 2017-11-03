using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionBase : MonoBehaviour {

    public enum ApplicationTarget { Player, Monster };
    public enum ApplicationTime { Always, TurnEnd };

    public ApplicationTarget applicationTarget;
    public ApplicationTime applicationTime;
    public int leftTurn;

    private void Awake()
    {
        
    }

    virtual public void ApplyCondition(Battle battle)
    {

    }

    virtual public void ApplyCondition(MonsterBase monsterBase)
	{
		
	}
}
