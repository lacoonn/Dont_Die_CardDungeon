using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public void Awake()
	{
		//GlobalDataManager.instance.scene = GlobalDataManager.Scene.Menu; // 현재 씬을 메뉴로 설정


	}

	// Use this for initialization
	void Start () {
		// 현재 카드가 아무것도 없을 경우 기본 세팅으로 설정
		if (GlobalDataManager.instance.currentCardList.Count == 0) {
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

		GlobalDataManager.instance.isInit = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
