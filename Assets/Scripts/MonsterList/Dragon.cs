﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonsterBase
{
	new private void Awake()
	{
		monsterName = "드래곤";
		description = "";
		baseMaxHp = 4000;
		baseAttackPoint = 200;
		baseArmor = 20;
		attackTurnInterval = 3;
	}
}
