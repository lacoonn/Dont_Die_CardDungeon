using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonsterBase
{
	public int incrementalUnit;

	private new void Awake()
	{
		baseMaxHp = 1000;
		baseAttackPoint = 375; // 턴당 125
		baseArmor = 0;
		attackTurnInterval = 3;
		monsterName = "오크";
		description = "공격력 " + baseAttackPoint + "\n다혈질 오크입니다. 공격 받을 때마다 공격력이 5% 증가합니다.";

		incrementalUnit = (int)(baseAttackPoint * 0.05); // 공격력 증가단위는 기본 공격력의 5%
	}

	public override void Attacked(int damage)
	{
		int realDamage = (damage - currentArmor);
		if (realDamage < 0)
			realDamage = 0;

		baseAttackPoint = baseAttackPoint + incrementalUnit;
		currentAttackPoint = baseAttackPoint;
		description = "공격력 " + currentAttackPoint + "\n다혈질 오크입니다. 공격 받을 때마다 공격력이 5% 증가합니다.";

		// 이펙트
		Vector3 tempVector = transform.position;
		tempVector.z -= (float)0.1;
		Instantiate(monsterSkillEffect, tempVector, Quaternion.identity);

		currentHp -= realDamage;
		if (currentHp <= 0)
		{
			currentHp = 0;
		}
	}
}
