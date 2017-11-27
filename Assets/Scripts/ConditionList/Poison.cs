using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : ConditionBase
{

    int damage;

    public Poison(ApplicationTarget _applicationTarget, int _leftTurn, int _damage)
    {
        applicationTarget = _applicationTarget;
        applicationTime = ApplicationTime.TurnEnd;
        leftTurn = _leftTurn;
        damage = _damage;
    }

	public override void ApplyCondition(MonsterBase monsterBase)
	{
		Vector3 location = BattleManager.instance.monsterPos.position;
		location.x += 1;
		location.y += 1;
		location.z = (int)(location.z - 0.1);
		BattleManager.instance.CreateSkillText("독 " + damage, location);

		monsterBase.currentHp -= damage;
        if (monsterBase.currentHp <= 0)
        {
            monsterBase.currentHp = 0;
            BattleManager.instance.EndBattle();
        }
		leftTurn--;
    }

    public override void ApplyCondition(BattleManager battleManager)
    {
		Vector3 location = BattleManager.instance.fieldCards[1].transform.position;
		location.y += 1;
		location.z = (int)(location.z - 0.1);
		BattleManager.instance.CreateSkillText("독 " + damage, location);

		battleManager.player.currentHp -= damage;
        if (battleManager.player.currentHp <= 0)
        {
            battleManager.player.currentHp = 0;
            BattleManager.instance.EndBattle();
        }
		leftTurn--;
    }
}
