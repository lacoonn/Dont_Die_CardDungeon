﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonsterBase
{
	new private void Awake()
	{
		monsterName = "고블린";
		description = "";
		baseMaxHp = 500;
		baseAttackPoint = 50;
		baseArmor = 0;
		attackTurnInterval = 1;
	}
}
