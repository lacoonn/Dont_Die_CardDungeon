using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour {

	public static GlobalDataManager instance = null;

	public int testNum = 111;

	public GameObject[] AllCardList;

	public GameObject[] CurrentCardList;

	void Awake()
	{
		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}
}
