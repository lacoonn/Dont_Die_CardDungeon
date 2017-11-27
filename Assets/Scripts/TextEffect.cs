using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffect : MonoBehaviour {
	public TextMesh textMesh;
	public Vector3 startPosition;
	public Vector3 endPosition;

	private void Awake()
	{
		textMesh = GetComponent<TextMesh>();
	}

	// Use this for initialization
	void Start () {
		
		startPosition = transform.position;
		Vector3 tempPosition = startPosition;
		tempPosition.y += 2;
		endPosition = tempPosition;

		Destroy(gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * 1);
	}

	public void SetText(string _text)
	{
		textMesh.text = _text;
	}
}