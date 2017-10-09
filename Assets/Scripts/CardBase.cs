using UnityEngine;
using System.Collections;

[System.Serializable]
public class CardBase : MonoBehaviour {
	public enum Seal
    {
		J,
		Q,
		K
	};
	public enum Job
    {
		Knight,
		Wizard,
		Priest
	};
	public enum Status
    {
        inField,
		inHand,
		inDeck,
		inTomb
	};
    public enum PositionIndex { Field0, Field1, Field2, Hand0, Deck };
    public int index = 0; // 카드가 속한 곳에서 몇번째에 있는지 저장하는 변수
		
	public Seal seal;
	public Job job;
    public Status status = Status.inDeck;
    public PositionIndex positionIndex; // 몇 번째 pos에 있는 카드인지 비교하기 위한 변수

    public string cardName;
	public string description;
	public int attack;
	public int heal;
    public bool GenerateRandomeData = false;
    public bool canPlay = false;

	public TextMesh attackText;
	public TextMesh healText;

	public Vector3 newPos;

	float distance_to_screen;
	bool Selected = false;

    public delegate void CustomAction();

    // Use this for initialization
    void Start () {
        //distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z - 1;
        distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

	void FixedUpdate()
	{
        // 카드가 선택된 상태가 아니라면 newPos로 이동하려는 성질을 가진다.
		if (!Selected)
		{
			transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 3);
			if (status != Status.inDeck)
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 180.0f, 0.0f), Time.deltaTime * 3);
			}
		}
	}

	public void PlaceCard()
	{
		if (status == Status.inHand && status == Status.inField)
		{
			//Selected = false;
			Battle.instance.PlaceCard(this);
		}
	}

	void OnMouseDown()
	{
		if (status == Status.inHand && status == Status.inField)
		{
			Selected = true;
		}
	}
	void OnMouseUp()
	{
        Debug.Log("onMouseUp()");
		Selected = false;
        // 다른 카드와 겹쳐있으면 그 카드와 원래의 위치를 변경
        Battle.instance.checkPlace(gameObject);

    }
	void OnMouseOver()
	{
		//Debug.Log("On Mouse Over Event");
	}
	void OnMouseEnter()
	{
		//Debug.Log("On Mouse Enter Event");
		//newPos += new Vector3(0,0.5f,0);
	}
	void OnMouseExit()
	{
		//Debug.Log("On Mouse Exit Event");
		//newPos -= new Vector3(0,0.5f, 0);
	}
	void OnMouseDrag()
	{
        Debug.Log("OnMouseDrag()");
        if (status == Status.inField || status == Status.inHand)
        {
            Vector3 tempVector3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            tempVector3.z = transform.position.z;
            transform.position = tempVector3;
        }
    }

	public void AttackMonster(CardBase attacker, MonsterBase target, bool addhistory, CustomAction action) // 몬스터를 공격!!
    {
		if (attacker.canPlay)
		{
			target.hp -= attacker.attack;

			action();
			if (addhistory)
				Battle.instance.AddHistory(attacker, target);
		}
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

	/*public void AddToHero(CardBehaviourScript magic, HeroBehaviourScript target, CustomAction action) // 카드에서 어떤 옵션을 다른 카드나 몬스터에 적용하는 함수 example
    {
		if (magic.canPlay)
		{
			target._Attack += magic.AddedAttack;
			if (target.health + magic.AddedHealth <= 30)
				target.health += magic.AddedHealth;
			else
				target.health = 30;
			action();
			Battle.instance.AddHistory(magic, target);
		}
	}

	public void AddToEnemies(CardBehaviourScript magic, List<CardBehaviourScript> targets, bool addhistory, CustomAction action) // 카드에서 어떤 옵션을 다른 카드나 몬스터에 적용하는 함수 example
    {
		if (magic.canPlay)
		{
			foreach (var target in targets)
			{
				AddToMonster(magic, target, addhistory, delegate { });
			}
			action();
		}
	}*/

	public object Clone()
	{
		CardBase temp = new CardBase();

        temp.seal = this.seal;
        temp.job = this.job;
        temp.status = this.status;
        temp.index = this.index;
		temp.cardName = this.cardName;
		temp.description = this.description;
		temp.attack = this.attack;
		temp.heal = this.heal;
        temp.GenerateRandomeData = this.GenerateRandomeData;
		temp.canPlay = this.canPlay;
		temp.newPos = this.newPos;
		temp.distance_to_screen = this.distance_to_screen;
		temp.Selected = this.Selected;

		return temp;
	}
}