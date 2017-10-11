using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterBase : MonoBehaviour
{
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
        Battle.instance.healthPoint -= attackPoint;
    }
}
