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

	public AllCardList allCardList = new AllCardList();

    public SaveData saveData;

    FileStream fileStream;


    void Awake()
	{
		if (instance)
		{
			DestroyImmediate (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);

        // Read savedata or init saveData variable
        if (File.Exists("savedata.xml"))
        {
            Debug.Log("Find save data");
            fileStream = new FileStream("savedata.xml", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            XmlReader xmlReader = XmlReader.Create(fileStream);
            if(fileStream.CanRead)
            {
                Debug.Log("Can read save data");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
                //
                Debug.Log("Deserialize start");
                saveData = (SaveData)xmlSerializer.Deserialize(xmlReader);
                Debug.Log("Deserialize end");
                //
                /*if (xmlSerializer.CanDeserialize(xmlReader))
                {
                    Debug.Log("Deserialize save data");
                    saveData = (SaveData)xmlSerializer.Deserialize(xmlReader);
                }
                else
                {
                    saveData = new SaveData();
                }*/
            }
            else
            {
                saveData = new SaveData();
            }
            fileStream.Close();
        }
        else
        {
            Debug.Log("Not exist save data");
            saveData = new SaveData();
        }

		instance.isInit = true;
	}

    private void OnApplicationQuit()
    {
        SaveDataToXml();
        Debug.Log("Save data finish");
    }

    public void SaveDataToXml()
    {
        // Save data before quit
        fileStream = new FileStream("savedata.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
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
