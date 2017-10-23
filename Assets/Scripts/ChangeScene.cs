using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ChangeSceneToBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void ChangeSceneToReward()
    {
        SceneManager.LoadScene("RewardScene");
    }
}
