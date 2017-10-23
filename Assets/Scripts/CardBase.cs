using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardBase : MonoBehaviour {
    // 각인
    public enum Seal { J, Q, K };
    public Seal seal;
    
    // 직업
    public enum Job { Knight, Wizard, Priest };
    public Job job;
    
    // 상태
    public enum Status { inField, inHand, inDeck, inTomb };
    public Status status = Status.inDeck;

    // 카드가 속한 상태 배열에서 몇 번째에 있는지 저장하는 변수
    public int index = 0;

    // 카드 이름 및 설명
    public string cardName;
	public string description;

    // 공격 후 카드 능력치 초기화에 사용되는 변수
    public int baseAttackPoint;
    public int baseHealPoint;
    public int baseHealthPoint;

    // 실제 적용되는 가드 능력치
    public int attackPoint;
	public int healPoint;
    public int healthPoint;

    // 텍스트 매쉬
	public TextMesh attackText;
	public TextMesh healText;

    // 카드의 위치가 수렴하는 장소(카드가 항상 이 장소로 이동하기 위해 움직임)
	public Vector3 newPos;

	float distanceToScreen;

    // 카드가 선택(터치)되어있는지 판별
	bool Selected = false;

    public delegate void CustomAction();

    // Use this for initialization
    public void Start ()
    {
        distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

    public void Update()
    {
        attackText.text = attackPoint.ToString();
        healText.text = healPoint.ToString();
    }

    public void FixedUpdate()
	{
        if (Battle.instance.gameState == Battle.GameState.Default)
        {
            // 카드가 선택된 상태가 아니라면 newPos로 이동하려는 성질을 가진다.
            if (!Selected)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 3);
                if (status == Status.inTomb || status == Status.inDeck)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 3);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 180.0f, 0.0f), Time.deltaTime * 3);
                }

            }
        }
        else if (Battle.instance.gameState == Battle.GameState.CardAttacking)
        {
            if (gameObject == Battle.instance.attackingCard)
            {
                float distance = Vector3.Distance(transform.position, Battle.instance.monsterPos.position);
                if (distance < 2)
                {
                    Debug.Log("One card attack end");
                    //Battle.instance.gameState = Battle.GameState.CardAttackEnd;
                    AttackMonster(Battle.instance.monster, null);
                    Battle.instance.gameState = Battle.GameState.CardAttackFinished;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, Battle.instance.monsterPos.position, Time.deltaTime * 3);
                }
            }
        }

    }

	void OnMouseDown()
	{
        if (Battle.instance.gameState == Battle.GameState.Default)
        {
            if (status == Status.inHand && status == Status.inField)
            {
                Selected = true;
            }
        }
	}

	void OnMouseUp()
	{
        if (Battle.instance.gameState == Battle.GameState.Default)
        {
            Selected = false;
            // 다른 카드와 겹쳐있으면 그 카드와 원래의 위치를 변경
            Battle.instance.CheckPlace(gameObject);
        }
    }

	void OnMouseDrag()
	{
        if (Battle.instance.gameState == Battle.GameState.Default)
        {
            if (status == Status.inField || status == Status.inHand)
            {
                Vector3 tempVector3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
                tempVector3.z = transform.position.z;
                transform.position = tempVector3;
            }
        }
    }

    public void AttackMonster(GameObject target, CustomAction action) // 몬스터를 공격!!
    {
        Debug.Log("Card attack monster!");
        // Attack monster
        target.GetComponent<MonsterBase>().Attacked(attackPoint);

        // Heal player
        Battle.instance.Healed(healPoint);

        //action();
        Battle.instance.AddHistory(this, target.GetComponent<MonsterBase>());
    }

	public void ApplyLeaderEffect()
	{
		
	}

    public void Destroy(CardBase card)
	{
		if (card)
		{
			if (card.gameObject != null)
			{
                //Battle.instance.fieldCards.Remove(card.gameObject);

				//BoardBehaviourScript.instance.PlaySound(BoardBehaviourScript.instance.cardDestroy);
				Destroy(card.gameObject);

			}

		}
        else
		{
			//card = null;
		}
	}
    
	public object Clone()
	{
        CardBase temp = new CardBase
        {
            seal = this.seal,
            job = this.job,
            status = this.status,
            index = this.index,
            cardName = this.cardName,
            description = this.description,
            attackPoint = this.attackPoint,
            healPoint = this.healPoint,
            healthPoint = this.healthPoint,
            newPos = this.newPos,
            distanceToScreen = this.distanceToScreen,
            Selected = this.Selected
        };

        return temp;
	}
}