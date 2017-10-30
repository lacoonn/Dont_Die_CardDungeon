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

    // 카드 활성화
    public bool isActive = false;

    public Status status = Status.inDeck;

    // 카드가 속한 상태 배열에서 몇 번째에 있는지 저장하는 변수
    public int index = 0;

    // 카드 이름 및 설명
    public string cardName;
	public string description;

    // 카드 레벨
    public int level = 1;

    // 공격 후 카드 능력치 초기화에 사용되는 변수
    public int baseAttackPoint;
    public int baseHealPoint;
    public int baseHealthPoint;

    // 실제 적용되는 가드 능력치
    public int attackPoint;
	public int healPoint;
    public int healthPoint;

    // 텍스트 매쉬
	public TextMesh nameText;
	public TextMesh descriptionText;
	public TextMesh attackText;
	public TextMesh healText;
	public TextMesh jobText;
	public TextMesh sealText;

    // 카드의 위치가 수렴하는 장소(카드가 항상 이 장소로 이동하기 위해 움직임)
	public Vector3 newPos;

	float distanceToScreen;

    // 카드가 선택(터치)되어있는지 판별
	bool Selected = false;

    public delegate void CustomAction();

	public void Awake()
	{
		distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
	}

	// Use this for initialization
	public void Start ()
    {
        // set seal textmesh
        if (seal == Seal.J)
            sealText.text = "J";
        else if (seal == Seal.Q)
            sealText.text = "Q";
        else
            sealText.text = "K";

        
    }

    public void Update()
    {
        attackText.text = attackPoint.ToString();
        healText.text = healPoint.ToString();
    }

	public void FixedUpdate()
	{
		if (isActive)
		{
			if (Battle.instance.gameState == Battle.GameState.CardAttackStart || Battle.instance.gameState == Battle.GameState.CardAttacking)
			{
				if (gameObject == Battle.instance.attackingCard)
				{
					float distance = Vector3.Distance(transform.position, Battle.instance.monsterPos.position);
					if (distance < 2)
					{
						Debug.Log("One card attack end");
						//Battle.instance.gameState = Battle.GameState.CardAttackEnd;
						AttackMonster(Battle.instance.monster, null);
						Battle.instance.gameState = Battle.GameState.CardAttackFinish;
					}
					else
					{
						transform.position = Vector3.Lerp(transform.position, Battle.instance.monsterPos.position, Time.deltaTime * 5);
					}
				}
			}
            else if (Battle.instance.gameState == Battle.GameState.CardAttackFinish)
            {

            }
			else
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
		}
		else if (GlobalDataManager.instance.scene == GlobalDataManager.Scene.Reward)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 180.0f, 0.0f), Time.deltaTime * 3);
		}
			
    }

	void OnMouseDown()
	{
		if (isActive) // Battle scene
		{
			if (Battle.instance.gameState == Battle.GameState.CardAttacking)
			{
				
			} 
			else
			{
				if (status == Status.inHand && status == Status.inField)
				{
					Selected = true;
				}
			}
		}
		else if (GlobalDataManager.instance.scene == GlobalDataManager.Scene.Reward)
		{

		}
	}

	void OnMouseUp()
	{
		if (isActive) // Battle scene
		{
			if (Battle.instance.gameState == Battle.GameState.CardAttacking)
			{
				
			}
			else
			{
				Selected = false;
				// 다른 카드와 겹쳐있으면 그 카드와 원래의 위치를 변경
				Battle.instance.CheckPlace(gameObject);
			}
		}
		else if (GlobalDataManager.instance.scene == GlobalDataManager.Scene.Reward)
		{
			// Check new cards
			for (int i = 0; i < 3; i++)
			{
				if (gameObject == RewardManager.instance.newCards[i])
				{
					RewardManager.instance.ClickNewCard(i);
				}
				if (gameObject == RewardManager.instance.currentSelectedCards[i])
				{
					RewardManager.instance.ClickCurrentCard(i);
				}
			}
			/*// Check current cards
			for (int i = 0; i < 9; i++)
			{

			}*/
		}
	}

	void OnMouseDrag()
	{
		if (isActive)
		{
			if (Battle.instance.gameState == Battle.GameState.CardAttacking)
			{
				
			}
			else
			{
				if (status == Status.inField || status == Status.inHand)
				{
					Vector3 tempVector3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
					tempVector3.z = transform.position.z;
					transform.position = tempVector3;
				}
			}
		}
		else if (GlobalDataManager.instance.scene == GlobalDataManager.Scene.Reward)
		{

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