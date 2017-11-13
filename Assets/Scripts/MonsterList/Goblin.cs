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
		description = "공격력 " + baseAttackPoint + "\n별거 없는 몬스터입니다.";
	}
}
