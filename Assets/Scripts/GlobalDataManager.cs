using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
	public static GlobalDataManager instance = null;

	public bool isInit = false;

	public enum Scene { Menu, Battle, Reward };

	public Scene scene = Scene.Menu;

	public AllCardList allCardList = new AllCardList();

	public List<string> currentCardList = new List<string>();
	

	void Awake()
	{
		if (instance)
		{
			DestroyImmediate (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);

		// 현재 카드가 아무것도 없을 경우 기본 세팅으로 설정
		if (instance.currentCardList.Count == 0)
		{
			GlobalDataManager.instance.currentCardList.Add("BeginnerKnight"); // 초보 기사
			GlobalDataManager.instance.currentCardList.Add("BeginnerKnight");
			GlobalDataManager.instance.currentCardList.Add("BeginnerKnight");
			GlobalDataManager.instance.currentCardList.Add("BeginnerWizard"); // 초보 마법사
			GlobalDataManager.instance.currentCardList.Add("BeginnerWizard");
			GlobalDataManager.instance.currentCardList.Add("BeginnerWizard");
			GlobalDataManager.instance.currentCardList.Add("BeginnerPriest"); // 초보 성직자
			GlobalDataManager.instance.currentCardList.Add("BeginnerPriest");
			GlobalDataManager.instance.currentCardList.Add("BeginnerPriest");
		}

		instance.isInit = true;
	}
}
