using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterBase : MonoBehaviour
{
	// 필요한 리소스
	protected GameObject monsterSkillEffect;
	protected GameObject monsterSkillText;

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

	public bool soundTrigger = false;

	public void Awake()
	{

	}

	// Use this for initialization
	public void Start()
    {
        monsterSkillEffect = Resources.Load("Prefabs/Effect/MonsterAttackEffect") as GameObject;
		monsterSkillText = Resources.Load("Prefabs/Effect/MonsterSkillText") as GameObject;

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

				// 사운드
				if (soundTrigger == false)
				{
					SoundManager.instance.PlayAttackSound();
					soundTrigger = true;
				}
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, homePosition, Time.deltaTime * 3);
				
				// 사운드
				soundTrigger = false;
			}
			
		}
    }

	public virtual void ResetMonsterStatus()
	{
		currentAttackPoint = baseAttackPoint;
		currentArmor = baseArmor;
		canWork = true;
	}

	public virtual bool TryToAttack()
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

    public virtual IEnumerator AttackPlayer()
    {
        Debug.Log(monsterName + " attack player!");

		BattleManager.instance.gameState = BattleManager.GameState.MonsterAttacking;

        Vector3 tempVector = transform.position;
        tempVector.z -= (float)0.1;

        yield return new WaitForSeconds(0.5f);

		turnLeftUntilAttack = attackTurnInterval;
        BattleManager.instance.player.Attacked(currentAttackPoint);

		BattleManager.instance.gameState = BattleManager.GameState.Default;
		speed = 3;
	}

    public virtual void Attacked(int damage)
    {
		int realDamage = (damage - currentArmor);
		if (realDamage < 0)
			realDamage = 0;

		currentHp -= realDamage;
		if (currentHp <= 0)
		{
			currentHp = 0;
		}
	}

	public virtual void CreateSkillEffect()
	{
		Vector3 tempVector = transform.position;
		tempVector.z -= (float)0.1;
		Instantiate(monsterSkillEffect, tempVector, Quaternion.identity);
	}

	public virtual void CreateSkillText(string text)
	{
		Vector3 tempVector = transform.position;
		tempVector.x += 2;
		tempVector.z -= (float)0.1;
		GameObject temp = Instantiate(monsterSkillText, tempVector, Quaternion.identity);
		temp.GetComponent<TextEffect>().SetText(text);
	}
}
