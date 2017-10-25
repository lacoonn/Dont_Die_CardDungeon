using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCardList
{
	public List<string> knightCardList = new List<string>();
	public List<string> wizardCardList = new List<string>();
	public List<string> priestCardList = new List<string>();

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
		Debug.Log("AllCardList");
	}

	private void Start()
	{
		
	}
}
