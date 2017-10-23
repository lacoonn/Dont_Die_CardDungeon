using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Leader effect created");
		Destroy (this, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
