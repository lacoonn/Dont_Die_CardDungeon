using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Screen.SetResolution(1080, 1920, true);

		//GlobalDataManager.instance.scene = GlobalDataManager.Scene.Menu; // 현재 씬을 메뉴로 설정
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectNewGameButton()
    {
        GlobalDataManager.instance.saveData = null;
        GlobalDataManager.instance.saveData = new SaveData();
		GlobalDataManager.instance.saveData.InitSaveData();

		GlobalDataManager.instance.ChangeSceneToBattle();
    }

    public void SelectExistingGameButton()
    {
        GlobalDataManager.instance.ChangeSceneToBattle();
    }
}
