﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : ConditionBase {

    int damage;

    public Poison(ApplicationTarget _applicationTarget, int _leftTurn, int _damage)
    {
        applicationTarget = _applicationTarget;
        applicationTime = ApplicationTime.TurnEnd;
        leftTurn = _leftTurn;
        damage = _damage;
    }

	override public void ApplyCondition(MonsterBase monsterBase)
	{
        monsterBase.currentHp -= damage;
        if (monsterBase.currentHp <= 0)
        {
            monsterBase.currentHp = 0;
            BattleManager.instance.EndBattle();
        }
		leftTurn--;
    }

    override public void ApplyCondition(BattleManager battleManager)
    {
        battleManager.player.currentHp -= damage;
        if (battleManager.player.currentHp <= 0)
        {
            battleManager.player.currentHp = 0;
            BattleManager.instance.EndBattle();
        }
		leftTurn--;
    }
}
