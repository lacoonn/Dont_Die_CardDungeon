using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public GameObject soundEffect;

	public AudioClip flipCard;
	AudioSource myAudio;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		soundEffect = Resources.Load("Prefabs/Sound/FlipCardSound") as GameObject;
	}

	// Use this for initialization
	void Start () {
		myAudio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayFlipCard()
	{
		myAudio.PlayOneShot(flipCard);
	}

	public void CreateSound()
	{
		Instantiate(soundEffect);
	}
}