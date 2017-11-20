using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonsterBase
{
	new private void Awake()
	{
		baseMaxHp = 500;
		baseAttackPoint = 100; // 턴당 100
		baseArmor = 0;
		attackTurnInterval = 1;
		monsterName = "고블린";
		description = "공격력 " + baseAttackPoint + "\n보잘 것 없는 고블린입니다.";
	}
}
