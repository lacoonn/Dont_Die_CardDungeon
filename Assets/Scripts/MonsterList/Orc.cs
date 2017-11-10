using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonsterBase
{
	new private void Awake()
	{
		monsterName = "오크";
		description = "";
		baseMaxHp = 1000;
		baseAttackPoint = 250; // 턴당 125
		baseArmor = 0;
		attackTurnInterval = 2;
	}
}
