using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public GameObject flipCard;
	public GameObject attack;

	//public AudioClip flipCard;
	
	//AudioSource myAudio;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		flipCard = Resources.Load("Prefabs/Sound/FlipCardSound") as GameObject;
		attack = Resources.Load("Prefabs/Sound/AttackSound") as GameObject;
	}

	// Use this for initialization
	void Start () {
		//myAudio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayFlipCardSound()
	{
		//myAudio.PlayOneShot(flipCard);
		Instantiate(flipCard);
	}

	public void PlayAttackSound()
	{
		Instantiate(attack);
	}
}