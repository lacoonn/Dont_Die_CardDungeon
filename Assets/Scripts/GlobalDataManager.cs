using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDataManager : MonoBehaviour
{
	public static GlobalDataManager instance = null;

    public enum Scene { Menu, Battle, Reward };

    public bool isInit = false;

	public Scene scene = Scene.Menu;

	public AllCardList allCardList;

	public SaveData saveData;


    void Awake()
	{
		if (instance)
		{
			DestroyImmediate (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);

        allCardList = new AllCardList();

		// Read savedata or init saveData variable
		if (File.Exists("savedata.xml"))
        {
            Debug.Log("Find save data");
            FileStream fileStream = new FileStream("savedata.xml", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            XmlReader xmlReader = XmlReader.Create(fileStream);
            if(fileStream.CanRead)
            {
                Debug.Log("Can read save data");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
                //
                Debug.Log("Deserialize start");
                saveData = (SaveData)xmlSerializer.Deserialize(xmlReader);
                Debug.Log("Deserialize end");
                Debug.Log(saveData.currentCardList.Count);
            }
            else
            {
                saveData = new SaveData();
                saveData.InitSaveData();
            }
            fileStream.Close();
        }
        else
        {
            Debug.Log("Not exist save data");
            saveData = new SaveData();
            saveData.InitSaveData();
        }

		instance.isInit = true;
	}

	public string GetStageMonsterName()
	{
		int stageNumber = saveData.stageNumber;

		if (1 <= stageNumber && stageNumber <= 9) // 1 ~ 9 층
		{
			int randomNumber = Random.Range(0, allCardList.FirstFloorMonsterList.Count - 1);
			return allCardList.FirstFloorMonsterList[randomNumber];
		}
		else if (10 <= stageNumber && stageNumber <= 10) // 10 층
		{
			int randomNumber = allCardList.FirstFloorMonsterList.Count - 1;
			return allCardList.FirstFloorMonsterList[randomNumber];
		}
		else if (11 <= stageNumber && stageNumber <= 19) // 11 ~ 19 층
		{
			int randomNumber = Random.Range(0, allCardList.SecondFloorMonsterList.Count - 1);
			return allCardList.SecondFloorMonsterList[randomNumber];
		}
		else if (20 <= stageNumber && stageNumber <= 20) // 20 층
		{
			int randomNumber = allCardList.SecondFloorMonsterList.Count - 1;
			return allCardList.SecondFloorMonsterList[randomNumber];
		}
		else // 에러
		{
			Debug.Log("Get Stage Monster Name Error! StageNumber : " + stageNumber);
			return null;
		}
	}

    private void OnApplicationQuit()
    {
        SaveDataToXml();
        Debug.Log("Save data finish");
    }

    public void SaveDataToXml()
    {
        // Save data before quit
        FileStream fileStream = new FileStream("savedata.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        XmlWriter xmlWriter = XmlWriter.Create(fileStream);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
        xmlSerializer.Serialize(xmlWriter, saveData);
    }

    public void ChangeSceneToMenu()
    {
        scene = Scene.Menu;
        SceneManager.LoadScene("MenuScene");
    }

    public void ChangeSceneToBattle()
    {
        scene = Scene.Battle;
        SceneManager.LoadScene("BattleScene");
    }

    public void ChangeSceneToReward()
    {
        scene = Scene.Reward;
        SceneManager.LoadScene("RewardScene");
    }
}
