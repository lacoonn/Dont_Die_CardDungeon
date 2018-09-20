using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCardList
{
	public List<string> knightCardList = new List<string>();
	public List<string> wizardCardList = new List<string>();
	public List<string> priestCardList = new List<string>();
	public List<string> monsterList = new List<string>();

	// 층별 몬스터
	public List<string>[] EachFloorMonsterList = new List<string>[5];

	public AllCardList()
	{
		// Knight Cards
		knightCardList.Add("BeginnerKnight");
		knightCardList.Add("Barbarian");
		knightCardList.Add("ArmorBreaker");
		knightCardList.Add("SpearKnight");

		// Wizard Cards
		wizardCardList.Add("BeginnerWizard");
		wizardCardList.Add("FreezingWizard");
		wizardCardList.Add("PoisonWizard");
		wizardCardList.Add("Sage");

		// Priest Cards
		priestCardList.Add("BeginnerPriest");
		priestCardList.Add("HealingPriest");
		priestCardList.Add("EncouragementPriest");
		priestCardList.Add("Paladin");

		// Monsters
		monsterList.Add("Goblin");
		monsterList.Add("GoblinAssassin");
		monsterList.Add("GoblinShaman");
		monsterList.Add("SkeletonSoldier");
		monsterList.Add("SkeletonKnight");
		monsterList.Add("SkeletonReaper");
		monsterList.Add("Orc");
		monsterList.Add("Golem");
		monsterList.Add("Dragon");

		// 1층 몬스터
		EachFloorMonsterList[0].Add("Goblin");
		EachFloorMonsterList[0].Add("Orc");
		EachFloorMonsterList[0].Add("Golem");
		EachFloorMonsterList[0].Add("GoblinAssassin");
		EachFloorMonsterList[0].Add("GoblinShaman");

		//2층 몬스터
		EachFloorMonsterList[1].Add("SkeletonSoldier");
		EachFloorMonsterList[1].Add("Orc");
		EachFloorMonsterList[1].Add("Golem");
		EachFloorMonsterList[1].Add("SkeletonKnight");
		EachFloorMonsterList[1].Add("SkeletonReaper");
	}
}
