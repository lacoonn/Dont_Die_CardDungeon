using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deprived : PlayerBase
{
	public Deprived()
	{
		name = "못 가진 자";
		description = "아무것도 가지지 못한 자. 모든 공격에 20%의 피해를 더 입는다.";
	}

	public override void Attacked(int damage)
	{
		//damage = (int)(damage * 1.2);
		damage = (int)(damage * 1.0);
		currentHp -= damage;
		if (currentHp <= 0)
		{
			currentHp = 0;
			BattleManager.instance.EndBattle();
		}
	}
}