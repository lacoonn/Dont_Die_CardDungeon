using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Battle : MonoBehaviour {
	public static Battle instance;

    public List<Transform> monsterPos = new List<Transform>();

    //public List<Transform> fieldPos = new List<Transform> ();
    //public Transform[] fieldPos = new Transform[3];
    public Transform[] fieldPos;

    public List<Transform> handPos = new List<Transform> ();
	public Transform deckPos;

    public Transform tombPos;



    public List<GameObject> monsters = new List<GameObject>();

    //public GameObject[] fieldCards = new List<GameObject> ();
    public GameObject[] fieldCards;
    public List<GameObject> handCards = new List<GameObject>();
    public List<GameObject> deckCards = new List<GameObject>();

    

	public TextMesh myHpText;

    public double combinationMagnification;
    public TextMesh combinationText;

    public bool gameStarted = true;
	public int turnNumber = 1;

	public CardBase currentCard;
    public MonsterBase targetMonster;

    public List<Hashtable> boardHistory = new List<Hashtable>();
    

    void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
    {
        // 모든 카드를 덱으로 이동
        int zPos = 0;
        foreach (GameObject gameObject in deckCards)
        {
            CardBase cardBase = gameObject.GetComponent<CardBase>();
            cardBase.status = CardBase.Status.inDeck;
            cardBase.newPos = deckPos.position;
            cardBase.newPos.z += zPos++; // 덱의 카드들의 위치가 겹치지 않도록 한다.
        }

        // 카드를 필드로 드로우
        for (int i = 0; i < 3; i++)
        {
            DrawCardFromDeck(CardBase.Status.inField);
        }
        // 카드를 핸드로 드로우
        DrawCardFromDeck(CardBase.Status.inHand);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddHistory(CardBase a, MonsterBase b)
    {
        Hashtable hash = new Hashtable();

        hash.Add(a, b);

        boardHistory.Add(hash);
        currentCard = null;
        targetMonster = null;
    }

    public void StartGame()
	{
        Debug.Log("StartGame()");
		gameStarted = true;
		UpdateGame();

		for (int i = 0; i < 3; i++)
		{
			DrawCardFromDeck(CardBase.Status.inField);
		}
	}

    // 카드를 덱에서 특정 Status로 드로우한다.
	public void DrawCardFromDeck(CardBase.Status goalStatus)
	{
        // 덱에 카드가 없으면 셔플
        if (deckCards.Count != 0)
            shuffleDeck();

        if (goalStatus == CardBase.Status.inField) // 필드로 드로우할 경우
        {
            Debug.Log(fieldPos.Length);
            // 모든 fieldCard를 검사해서 NULL이 있으면 그것의 자리를 메운다
            for (int i = 0; i < fieldCards.Length; i++)
            {
                if (fieldCards[i] == null)
                {
                    GameObject tempCard = deckCards[0];
                    deckCards.Remove(tempCard);

                    tempCard.GetComponent<CardBase>().newPos = fieldPos[i].position;
                    tempCard.GetComponent<CardBase>().status = CardBase.Status.inField;
                    tempCard.GetComponent<CardBase>().index = i;
                    if (i == 0)
                        tempCard.GetComponent<CardBase>().positionIndex = CardBase.PositionIndex.Field0;
                    if (i == 1)
                        tempCard.GetComponent<CardBase>().positionIndex = CardBase.PositionIndex.Field1;
                    if (i == 2)
                        tempCard.GetComponent<CardBase>().positionIndex = CardBase.PositionIndex.Field2;
                    fieldCards[i] = tempCard;
                }
            }
        }
        else if (goalStatus == CardBase.Status.inHand)
        {
            GameObject tempCard = deckCards[0];
            deckCards.Remove(tempCard);

            tempCard.GetComponent<CardBase>().newPos = handPos[0].position;
            tempCard.GetComponent<CardBase>().status = CardBase.Status.inHand;
            tempCard.GetComponent<CardBase>().positionIndex = CardBase.PositionIndex.Hand0;

            handCards.Add(tempCard);
        }
        UpdateGame();
    }

    public void shuffleDeck()
    {

    }

	/*public void checkPlace(CardBase cardBase) // 카드의 위치가 다른 카드와 겹치는지, 묘지와 겹치는지 확인한다.
    {
        Vector3 currentCardPosition = cardBase.transform.position;
        currentCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다

        // 필드의 다른 카드와 겹치는지 확인
        foreach (GameObject tempCard in deckCards)
        {
            Vector3 tempCardPosition = tempCard.transform.position;
            tempCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다
            float distance = Vector3.Distance(currentCardPosition, tempCardPosition);
        }
        // 핸드의 다른 카드와 겹치는지 확인

        // 묘지와 겹치는지 확인


    }*/

    public void checkPlace(GameObject cardObject) // 카드의 위치가 다른 카드와 겹치는지, 묘지와 겹치는지 확인한다.
    {
        Vector3 currentCardPosition = cardObject.transform.position;
        currentCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다

        Vector3 tempCardPosition;
        float distance;

        // 필드의 다른 카드와 겹치는지 확인
        foreach (GameObject tempCard in fieldCards)
        {
            if (tempCard != cardObject)
            {
                tempCardPosition = tempCard.transform.position;
                tempCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다
                distance = Vector3.Distance(currentCardPosition, tempCardPosition);
                if (distance < 1)
                {
                    Debug.Log("Swap with field");
                    swapCards(cardObject, tempCard); // 카드 교체
                    return;
                }
            }
            else
            {
                Debug.Log("Same Card(생략)");
            }
        }
        // 핸드의 다른 카드와 겹치는지 확인
        Debug.Log(handCards.Count);
        tempCardPosition = handCards[0].transform.position;
        tempCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다
        distance = Vector3.Distance(currentCardPosition, tempCardPosition);
        if (distance < 1)
        {
            Debug.Log("Swap with hand");
            swapCards(cardObject, handCards[0]); // 카드 교체
            return;
        }
        // 묘지와 겹치는지 확인
        tempCardPosition = tombPos.transform.position;
        tempCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다
        distance = Vector3.Distance(currentCardPosition, tempCardPosition);
        if (distance < 1)
        {
            Debug.Log("Card to tomb");
            // 카드 버림
            return;
        }

    }

    public void swapCards(GameObject card0, GameObject card1)
    {
        GameObject tempCard = Instantiate(card1) as GameObject;
        Debug.Log(card1.GetComponent<CardBase>().status);
        // 카드 1을 0의 자리로
        if (card0.GetComponent<CardBase>().status == CardBase.Status.inField)
        {
            Debug.Log("hand to field");
            fieldCards[card0.GetComponent<CardBase>().index] = card1;
            fieldCards[card0.GetComponent<CardBase>().index].GetComponent<CardBase>().newPos = card0.GetComponent<CardBase>().newPos;
            //카드 0의 속성과 같아야 하는 부분(포지션, 상태, 인덱스)
            //fieldCards[card0.GetComponent<CardBase>().index].transform.position = card0.transform.position;
            fieldCards[card0.GetComponent<CardBase>().index].GetComponent<CardBase>().status = card0.GetComponent<CardBase>().status;
            fieldCards[card0.GetComponent<CardBase>().index].GetComponent<CardBase>().index = card0.GetComponent<CardBase>().index;
        }
        else
        {
            handCards[0] = card1;
            handCards[0].GetComponent<CardBase>().newPos = card0.GetComponent<CardBase>().newPos;
            //카드 0의 속성과 같아야 하는 부분(포지션, 상태)
            //handCards[0].transform.position = card0.transform.position;
            handCards[0].GetComponent<CardBase>().status = card0.GetComponent<CardBase>().status;
            handCards[0].GetComponent<CardBase>().index = card0.GetComponent<CardBase>().index;
        }
        Debug.Log(card1.GetComponent<CardBase>().status);
        // 카드 0을 1의 자리로
        if (tempCard.GetComponent<CardBase>().status == CardBase.Status.inField) 
        {
            fieldCards[tempCard.GetComponent<CardBase>().index] = card0;
            //카드 1의 속성과 같아야 하는 부분(뉴포즈, 상태, 인덱스)
            fieldCards[tempCard.GetComponent<CardBase>().index].GetComponent<CardBase>().newPos = tempCard.GetComponent<CardBase>().newPos;
            //fieldCards[tempCard.GetComponent<CardBase>().index].transform.position = tempCard.transform.position;
            fieldCards[tempCard.GetComponent<CardBase>().index].GetComponent<CardBase>().status = tempCard.GetComponent<CardBase>().status;
            fieldCards[tempCard.GetComponent<CardBase>().index].GetComponent<CardBase>().index = tempCard.GetComponent<CardBase>().index;
        }
        else
        {
            Debug.Log("field to hand");
            handCards[0] = card0;
            //카드 1의 속성과 같아야 하는 부분(뉴포즈, 상태, 인덱스)
            handCards[0].GetComponent<CardBase>().newPos = tempCard.GetComponent<CardBase>().newPos;
            //handCards[0].transform.position = tempCard.transform.position;
            handCards[0].GetComponent<CardBase>().status = tempCard.GetComponent<CardBase>().status;
            handCards[0].GetComponent<CardBase>().index = tempCard.GetComponent<CardBase>().index;
        }
        
        Destroy(tempCard);
    }

    /*void drawMyLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
	{

		StartCoroutine(drawLine(start, end, color, duration));

	}
	IEnumerator drawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Additive"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetVertexCount(3);
		lr.SetPosition(0, start);
		lr.SetPosition(1,(( (-start+ end)*0.5f+start))+new Vector3(0,0,-5.0f));
		lr.SetPosition(2, end);
		yield return new WaitForSeconds(duration);
		GameObject.Destroy(myLine);
	}*/

    void UpdateGame()
	{
		//UpdateBoard();
	}

	public void PlaceCard(CardBase card)
	{
        /*
		if (card.team == CardBehaviourScript.Team.My && MyMana - card.mana >= 0 && MyTableCards.Count < 10)
		{
			//card.gameObject.transform.position = MyTablePos.position;
			card.GetComponent<CardBehaviourScript>().newPos = MyTablePos.position;

			MyHandCards.Remove(card.gameObject);
			MyTableCards.Add(card.gameObject);

			card.SetCardStatus(CardBehaviourScript.CardStatus.OnTable);
			//PlaySound(cardDrop);

			if (card.cardtype == CardBehaviourScript.CardType.Magic)///Apply Magic Effect 
			{
				card.canPlay = true;
				if (card.cardeffect == CardBehaviourScript.CardEffect.ToAll)
				{
					card.AddToAll(card,true, delegate { card.Destroy(card); });
				}
				else if (card.cardeffect == CardBehaviourScript.CardEffect.ToEnemies)
				{
					card.AddToEnemies(card,AITableCards,true, delegate { card.Destroy(card); });
				}
			}

			MyMana -= card.mana;
		}

		if (card.team == CardBehaviourScript.Team.AI && AIMana - card.mana >= 0 && AITableCards.Count < 10)
		{
			//card.gameObject.transform.position = AITablePos.position;
			card.GetComponent<CardBehaviourScript>().newPos = AITablePos.position;

			AIHandCards.Remove(card.gameObject);
			AITableCards.Add(card.gameObject);

			card.SetCardStatus(CardBehaviourScript.CardStatus.OnTable);
			//PlaySound(cardDrop);

			if (card.cardtype == CardBehaviourScript.CardType.Magic)///Apply Magic Effect 
			{
				card.canPlay = true;
				if (card.cardeffect == CardBehaviourScript.CardEffect.ToAll)
				{
					card.AddToAll(card,true, delegate { card.Destroy(card); });
				}
				else if (card.cardeffect == CardBehaviourScript.CardEffect.ToEnemies)
				{
					card.AddToEnemies(card,MyTableCards,true, delegate { card.Destroy(card); });
				}
			}

			AIMana -= card.mana;
		}*/

        UpdateGame();
	}
    /*
	public void PlaceRandomCard(CardBehaviourScript.Team team)
	{
		if (team == CardBehaviourScript.Team.My && MyHandCards.Count != 0)
		{
			int random = Random.Range(0, MyHandCards.Count);
			GameObject tempCard = MyHandCards[random];

			PlaceCard(tempCard.GetComponent<CardBehaviourScript>());
		}

		if (team == CardBehaviourScript.Team.AI && AIHandCards.Count != 0)
		{
			int random = Random.Range(0, AIHandCards.Count);
			GameObject tempCard = AIHandCards[random];

			PlaceCard(tempCard.GetComponent<CardBehaviourScript>());
		}

		UpdateGame();
		EndTurn();

		TablePositionUpdate();
		HandPositionUpdate();
	}
	public void EndGame(HeroBehaviourScript winner)
	{
		if (winner == MyHero)
		{
			Debug.Log("MyHero");
			Time.timeScale = 0;
			winnertext.text = "You Won";
			//Destroy(this);
		}

		if (winner == AIHero)
		{
			Time.timeScale = 0;
			Debug.Log("AIHero");
			winnertext.text = "You Losse";
			//Destroy(this);
		}
	}
	void OnGUI()
	{
		if (gameStarted)
		{
			if (turn == Turn.MyTurn)
			{
				if (GUI.Button(new Rect(Screen.width - 200, Screen.height / 2 - 50, 100, 50), "End Turn"))
				{
					EndTurn();
				}
			}

			GUI.Label(new Rect(Screen.width-200, Screen.height / 2 - 100, 100, 50), "Turn: " + turn + " Turn Number: " + turnNumber.ToString());

			foreach (Hashtable history in boardHistory)
			{
				foreach (DictionaryEntry entry in history)
				{
					CardGameBase card1 = entry.Key as CardGameBase;
					CardGameBase card2 = entry.Value as CardGameBase;

					GUILayout.Label(card1._name + " > " + card2._name);
				}
			}
			if (boardHistory.Count > 25)
			{
				Hashtable temp;
				temp = boardHistory[boardHistory.Count - 1];
				boardHistory.Clear();
				boardHistory.Add(temp);
			}
		}
	}*/
	void EndTurn()
	{
		turnNumber += 1;

		OnNewTurn();
	}

	void OnNewTurn()
	{
		UpdateGame();
	}
	
	void OnTriggerEnter(Collider Obj)
	{
		CardBase card = Obj.GetComponent<CardBase>();
		if (card)
		{
			card.PlaceCard();
		}

	}
}