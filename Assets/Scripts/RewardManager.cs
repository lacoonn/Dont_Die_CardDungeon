using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour {

	public GameObject sceneManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// excuted when progress button is clicked
	public void ClickProgressButton()
	{
		sceneManager.GetComponent<ChangeScene> ().ChangeSceneToMenu ();
	}
}
