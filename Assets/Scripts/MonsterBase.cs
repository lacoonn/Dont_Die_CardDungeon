using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterBase : MonoBehaviour
{
	// 필요한 리소스
	public GameObject monsterAttackEffect;

    public string monsterName;
    public string discription;

    public int healthPoint;
    public int maxHealthPoint;

    public int attackPoint;


    public TextMesh healthText;

    public Vector3 homePosition;

    // Use this for initialization
    public void Start()
    {
        healthPoint = maxHealthPoint;
    }

    public void Update()
    {
        healthText.text = healthPoint.ToString() + "/" + maxHealthPoint.ToString();
    }

    public void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, homePosition, Time.deltaTime * 3);
    }

    public void AttackPlayer()
    {
        Debug.Log("Monster attack player!");
		StartCoroutine (ShowAttackEffect ());
        //Battle.instance.Attacked(attackPoint);
    }

	IEnumerator ShowAttackEffect()
	{
		Vector3 tempVector = this.transform.position;
		tempVector.z -= 10;
		Instantiate (monsterAttackEffect, tempVector, Quaternion.identity);
		yield return new WaitForSeconds (1f);
		Battle.instance.Attacked(attackPoint);
		// 코루틴 뒤에 더 이상 작동할 코드가 없다면 쉬지않음
	}

    public void Attacked(int damage)
    {
        healthPoint -= damage;
        if (healthPoint < 0)
        {
            healthPoint = 0;
            Battle.instance.EndBattle();
        }
    }
}
