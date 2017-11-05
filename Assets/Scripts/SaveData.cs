using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public struct CardData
    {
        public string cardName;
        public int level;

        public CardData(string _cardName, int _level)
        {
            cardName = _cardName;
            level = _level;
        }
    }

    // variables
    public int stageNumber;
    public List<CardData> currentCardList;

    public void InitSaveData()
    {
        stageNumber = 1;
        currentCardList = new List<CardData>();

        // 현재 카드가 아무것도 없을 경우 기본 세팅으로 설정
        if (currentCardList.Count == 0)
        {
            currentCardList.Add(new SaveData.CardData("BeginnerKnight", 1)); // 초보 기사
            currentCardList.Add(new SaveData.CardData("BeginnerKnight", 1));
            currentCardList.Add(new SaveData.CardData("BeginnerKnight", 1));

            currentCardList.Add(new SaveData.CardData("BeginnerWizard", 1)); // 초보 마법사
            currentCardList.Add(new SaveData.CardData("BeginnerWizard", 1));
            currentCardList.Add(new SaveData.CardData("BeginnerWizard", 1));

            currentCardList.Add(new SaveData.CardData("BeginnerPriest", 1)); // 초보 성직자
            currentCardList.Add(new SaveData.CardData("BeginnerPriest", 1));
            currentCardList.Add(new SaveData.CardData("BeginnerPriest", 1));
        }
    }
}
