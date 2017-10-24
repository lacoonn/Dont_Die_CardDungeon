using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneToMenu()
    {
		GlobalDataManager.instance.scene = GlobalDataManager.Scene.Menu;
		SceneManager.LoadScene("MenuScene");
    }

    public void ChangeSceneToBattle()
    {
		GlobalDataManager.instance.scene = GlobalDataManager.Scene.Battle;
		SceneManager.LoadScene("BattleScene");
    }

    public void ChangeSceneToReward()
    {
		GlobalDataManager.instance.scene = GlobalDataManager.Scene.Reward;
		SceneManager.LoadScene("RewardScene");
    }
}
