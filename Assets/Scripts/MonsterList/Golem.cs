using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonsterBase
{
	new private void Awake()
	{
		monsterName = "골렘";
		description = "";
		baseMaxHp = 1000;
		baseAttackPoint = 450; // 턴당 150
		baseArmor = 50;
		attackTurnInterval = 3;
	}
}
