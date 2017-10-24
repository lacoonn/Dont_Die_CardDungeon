using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
	public static GlobalDataManager instance = null;

	public bool isInit = false;

	public enum Scene { Menu, Battle, Reward };

	public Scene scene = Scene.Menu;

	public int testNum = 111;

	public List<string> AllCardList;

	public List<string> currentCardList;
	

	void Awake()
	{
		if (instance)
		{
			DestroyImmediate (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}
}
