using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCardList
{
	public List<string> knightCardList = new List<string>();
	public List<string> wizardCardList = new List<string>();
	public List<string> priestCardList = new List<string>();
	public List<string> monsterList = new List<string>();

	// 1층 몬스터
	public List<string> FirstFloorMonsterList = new List<string>();
	// 2층 몬스터
	public List<string> SecondFloorMonsterList = new List<string>();
	// 3층 몬스터
	public List<string> ThirdFloorMonsterList = new List<string>();

	public AllCardList()
	{
		// Knight Cards
		knightCardList.Add("BeginnerKnight");
		knightCardList.Add("Barbarian");
		knightCardList.Add("ArmorBreaker");

		// Wizard Cards
		wizardCardList.Add("BeginnerWizard");
		wizardCardList.Add("FreezingWizard");
		wizardCardList.Add("PoisonWizard");

		// Priest Cards
		priestCardList.Add("BeginnerPriest");
		priestCardList.Add("HealingPriest");
		priestCardList.Add("EncouragementPriest");

		// Monsters
		monsterList.Add("Goblin");
		monsterList.Add("Orc");
		monsterList.Add("Golem");
		monsterList.Add("Dragon");

		// 1층 몬스터
		FirstFloorMonsterList.Add("Goblin");
		FirstFloorMonsterList.Add("GoblinAssassin");
		FirstFloorMonsterList.Add("GoblinShaman");

		//2층 몬스터
		SecondFloorMonsterList.Add("SkeletonSoldier");
		SecondFloorMonsterList.Add("SkeletonKnight");
		SecondFloorMonsterList.Add("SkeletonReaper");
	}
}
