using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase
{
	// 플레이어 이름 및 설명
	public string name;
	public string description;
	// 체력 변수
	public int currentHp;
	public int maxHp;

	public virtual void Attacked(int damage)
	{
		currentHp -= damage;
		if (currentHp <= 0)
		{
			currentHp = 0;
		}
	}

	public virtual void Healed(int healPoint)
	{
		currentHp += healPoint;
		if (currentHp > maxHp)
			currentHp = maxHp;
	}
}
