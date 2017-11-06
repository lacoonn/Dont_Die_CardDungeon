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
		baseAttackPoint = 100;
		baseArmor = 50;
		attackTurnInterval = 3;
	}
}
