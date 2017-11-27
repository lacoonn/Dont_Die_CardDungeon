using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonsterBase
{
	public int debrisDamage = 30;
	private new void Awake()
	{
		baseMaxHp = 1000;
		baseAttackPoint = 600; // 턴당 150
		baseArmor = 50;
		attackTurnInterval = 4;

		// debrisDamage = *;

		monsterName = "골렘";
		description = "진흙으로 빚어진 골렘입니다. 피격 시 파편이 튀어 " + debrisDamage + "의 피해를 줍니다.";
	}

	public override void Attacked(int damage)
	{
		int realDamage = (damage - currentArmor);
		if (realDamage < 0)
			realDamage = 0;

		BattleManager.instance.player.Attacked(debrisDamage);
		// 이펙트
		CreateSkillEffect();
		CreateSkillText("파편");

		currentHp -= realDamage;
		if (currentHp <= 0)
		{
			currentHp = 0;
		}
	}
}
