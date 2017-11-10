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
	public TextMesh armorText;

	public Vector3 homePosition;

	public int speed = 3;

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
		turnLeftUntilAttack = attackTurnInterval;

		ResetMonsterStatus();
    }

    public void Update()
    {
		if (GlobalDataManager.instance.scene == GlobalDataManager.Scene.Battle)
		{
			healthText.text = currentHp.ToString() + "/" + maxHp.ToString();
			turnLeftUntilAttackText.text = turnLeftUntilAttack.ToString();
			armorText.text = currentArmor.ToString();
		}
	}

    public void FixedUpdate()
    {
		if (GlobalDataManager.instance.scene == GlobalDataManager.Scene.Battle)
		{
			if (BattleManager.instance.gameState == BattleManager.GameState.MonsterAttacking)
			{
				float step = speed * Time.deltaTime;
				Vector3 target = BattleManager.instance.monsterPos.position;
				target.y -= 1;
				transform.position = Vector3.Lerp(transform.position, target, step);
				//transform.position = Vector3.MoveTowards(transform.position, target, step);
				speed += 2;
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, homePosition, Time.deltaTime * 3);
			}
			
		}
    }

	public virtual void ResetMonsterStatus()
	{
		currentAttackPoint = baseAttackPoint;
		currentArmor = baseArmor;
		canWork = true;
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
                return false;
            }
        }
    }

    public IEnumerator AttackPlayer()
    {
        Debug.Log("Monster attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

        Vector3 tempVector = transform.position;
        tempVector.z -= (float)0.1;
        Instantiate(monsterAttackEffect, tempVector, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        turnLeftUntilAttack = attackTurnInterval;
        BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
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
			BattleManager.instance.EndBattle();
		}
	}
}
