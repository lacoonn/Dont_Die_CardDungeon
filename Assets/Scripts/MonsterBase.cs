using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterBase : MonoBehaviour
{
	// 필요한 리소스
	private GameObject monsterAttackEffect;

    public string monsterName;
    public string description;

    // base 속성
    public int baseMaxHp;
    public int baseAttackPoint;
	public int baseArmor;
	public int attackTurnInterval;

    // 현재 게임 속성
    public int maxHp;
    public int currentHp;
    public int currentAttackPoint;
	public int currentArmor;
	public int turnLeftUntilAttack;

    public bool canWork;

    public TextMesh healthText;
    public TextMesh turnLeftUntilAttackText;

    public Vector3 homePosition;

	public void Awake()
	{

	}

	// Use this for initialization
	public void Start()
    {
        monsterAttackEffect = Resources.Load("Prefabs/Effect/MonsterAttackEffect") as GameObject;
        // base -> current
        maxHp = baseMaxHp;
        currentHp = maxHp;
        currentAttackPoint = baseAttackPoint;
		currentArmor = baseArmor;
        turnLeftUntilAttack = attackTurnInterval;
        canWork = true;
    }

    public void Update()
    {
        healthText.text = currentHp.ToString() + "/" + maxHp.ToString();
    }

    public void FixedUpdate()
    {
        if (GlobalDataManager.instance.scene == GlobalDataManager.Scene.Battle)
            transform.position = Vector3.Lerp(transform.position, homePosition, Time.deltaTime * 3);
    }

    public bool TryToAttack()
    {
        if(turnLeftUntilAttack <= 1)
        {
            if (canWork)
            {
                return true;
            }
            else
            {
                canWork = true;
                return false;
            }
        }
        else
        {
            if (canWork)
            {
                turnLeftUntilAttack--;
                return false;
            }
            else
            {
                canWork = true;
                return false;
            }
        }
    }

    public IEnumerator AttackPlayer()
    {
        Debug.Log("Monster attack player!");
        Vector3 tempVector = transform.position;
        tempVector.z -= (float)0.1;
        Instantiate(monsterAttackEffect, tempVector, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        turnLeftUntilAttack = attackTurnInterval;
        Battle.instance.Attacked(currentAttackPoint);
    }

    virtual public void Attacked(int damage)
    {
		int realDamage = (damage - currentArmor);
		if (realDamage < 0)
			realDamage = 0;

		currentHp -= realDamage;
		if (currentHp <= 0)
		{
			currentHp = 0;
			Battle.instance.EndBattle();
		}
	}
}
