using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffect : MonoBehaviour {
	public TextMesh textMesh;

	private void Awake()
	{
		textMesh = GetComponent<TextMesh>();
	}

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetText(string _text)
	{
		textMesh.text = _text;
	}
}