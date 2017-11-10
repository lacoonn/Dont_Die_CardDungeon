using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase
{
	// 체력 변수
	public int currentHp;
	public int maxHp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Attacked(int damage)
	{
		currentHp -= damage;
		if (currentHp <= 0)
		{
			currentHp = 0;
			BattleManager.instance.EndBattle();
		}
	}

	public virtual void Healed(int healPoint)
	{
		currentHp += healPoint;
		if (currentHp > maxHp)
			currentHp = maxHp;
	}
}
