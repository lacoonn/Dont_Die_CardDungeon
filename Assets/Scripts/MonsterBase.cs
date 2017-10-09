using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    public string monsterName;
    public string discription;
    public int hp;
    public int attack;

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (hp <= 0)
        {
            Destroy(this);
        }
    }
}
