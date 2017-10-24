using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Battle : MonoBehaviour {
    // 배틀 인스턴스
	public static Battle instance;

	// 사용할 오브젝트들
    public GameObject sceneManager;
	public GameObject leaderEffect;

    // 조합 배율
    public int DOUBLE = 2; // Seal이 같은 카드가 두 장
    public int TRIPLE = 5; // Seal이 같은 카드가 세 장
    public int STRAIGHT = 3; // Seal이 종류별로 세 장
    public int DUO = 2; // 직업이 같은 카드가 두 장
    public int TRIO = 5; // 직업이 같은 카드가 세 장
    public int COLLABORATION = 3; // 직업이 종류별로 세 장

    // 게임 상태
    public enum GameState { Default, ShuffleStart, Shuffling, ShuffleEnd, CardAttackStart, CardAttacking, CardAttackFinish, MonsterAttacking };
    public GameState gameState;

    // 포지션 관련 변수
    public Transform monsterPos;
    public Transform[] fieldPos;
    public Transform[] handPos;
	public Transform deckPos;
    public Transform tombPos;

    // 게임 오브젝트 변수
    public GameObject monster;
    public GameObject[] fieldCards;
    public GameObject[] handCards;
    public List<GameObject> deckCards = new List<GameObject>();
    public List<GameObject> tombCards = new List<GameObject>();

    // 체력 변수
    public int healthPoint = 0;
    public int maxHealthPoint = 0;
    
    // 조합 배율 변수
    public float baseCombination = 1;
    public float combination = 1;

    // 텍스트 매쉬
    public TextMesh healthText;
    public TextMesh combinationText;

    public bool gameStarted = true;

    // 턴
	public int turnNumber = 1;

    // 현재 공격중인 카드
    public GameObject attackingCard;

	public CardBase currentCard;
    public MonsterBase targetMonster;

    public List<Hashtable> boardHistory = new List<Hashtable>();

    void Awake()
	{
		instance = this;

		// 게임스테이트 초기화
		gameState = GameState.Default;

		// 몬스터 초기화
		monster.transform.position = monsterPos.position;
		monster.GetComponent<MonsterBase>().homePosition = monsterPos.position;

		// 모든 카드를 덱으로 이동
		int zPos = 0;
		for (int i = 0; i < 9; i++)
		{
			//Debug.Log(Resources.Load("Prefabs/Card/" + GlobalDataManager.instance.currentCardNameList[i]) as GameObject);
			GameObject gameObject = Instantiate(Resources.Load("Prefabs/Card/" + GlobalDataManager.instance.currentCardList[i]) as GameObject, new Vector3(0, 0, 0), Quaternion.identity); // should instantiate after load resources
			CardBase cardBase = gameObject.GetComponent<CardBase>();
			cardBase.isActive = true;
			// 각인 설정
			if (i % 3 == 0)
				cardBase.seal = CardBase.Seal.J;
			else if (i % 3 == 1)
				cardBase.seal = CardBase.Seal.Q;
			else
				cardBase.seal = CardBase.Seal.K;

			// 공, 체, 힐을 기본으로 변경
			cardBase.attackPoint = cardBase.baseAttackPoint;
			cardBase.healPoint = cardBase.baseHealPoint;
			cardBase.healthPoint = cardBase.baseHealthPoint;

			cardBase.status = CardBase.Status.inTomb;
			cardBase.newPos = tombPos.position;
			cardBase.newPos.z += zPos++; // 덱의 카드들의 위치가 겹치지 않도록 한다.

			// 플레이어 체력에 카드 체력 추가
			maxHealthPoint += cardBase.healthPoint;

			// 덱 카드에 오브젝트 할당
			tombCards.Add(gameObject);
		}
		healthPoint = maxHealthPoint;

		// 텍스트매쉬 초기화
		healthText.text = healthPoint.ToString();
		combinationText.text = combination.ToString();
	}

	// Use this for initialization
	void Start ()
    {
		StartCoroutine(StartGame());
    }

    void Update()
    {
		if (gameState == GameState.ShuffleStart) {
			gameState = GameState.Shuffling;
		} else if (gameState == GameState.Shuffling) {
			
		} else if (gameState == GameState.ShuffleEnd) {
			
		}
			
    }

    void FixedUpdate()
    {
        // 텍스트매쉬 초기화
        healthText.text = healthPoint.ToString() + "/" + maxHealthPoint.ToString();
        combinationText.text = combination.ToString();
    }

    public void AddHistory(CardBase a, MonsterBase b)
    {
        Hashtable hash = new Hashtable();

        hash.Add(a, b);

        boardHistory.Add(hash);
        currentCard = null;
        targetMonster = null;
    }

    public IEnumerator StartGame()
	{
        Debug.Log("StartGame()");
		gameStarted = true;

		Debug.Log("ShuffleDeck Start");
		// Shuffle cards and draw from deck
		StartCoroutine(ShuffleDeck());
		while (gameState != GameState.ShuffleEnd) {
			yield return new WaitForSeconds (0.1f);
		}
		Debug.Log("ShuffleDeck End");
		for (int i = 0; i < 3; i++)
		{
			Debug.Log ("LoL" + i);
			StartCoroutine(DrawCardFromDeck(CardBase.Status.inField));
			yield return new WaitForSeconds (0.1f);
		}
		// 카드를 핸드로 드로우
		StartCoroutine(DrawCardFromDeck(CardBase.Status.inHand));
	}

	// 카드를 덱에서 특정 Status로 드로우한다.
	public IEnumerator DrawCardFromDeck(CardBase.Status goalStatus)
	{
		GameObject tempCard;

		if (deckCards.Count == 0) // 덱에 카드가 없으면 셔플
		{
			StartCoroutine(ShuffleDeck());

			yield return new WaitForSeconds(1f);
		}

		if (goalStatus == CardBase.Status.inField) // 필드로 드로우할 경우
		{
			// 모든 fieldCard를 검사해서 NULL이 있으면 그것의 자리를 메운다
			for (int i = 0; i < fieldCards.Length; i++)
			{
				if (fieldCards[i] == null)
				{
					tempCard = deckCards[0];
					deckCards.Remove(tempCard);

					tempCard.GetComponent<CardBase>().newPos = fieldPos[i].position;
					tempCard.GetComponent<CardBase>().status = CardBase.Status.inField;
					tempCard.GetComponent<CardBase>().index = i;

					fieldCards[i] = tempCard;

					break;
				}
			}
		}
		else if (goalStatus == CardBase.Status.inHand)
		{
			for (int i = 0; i < handCards.Length; i++)
			{
				if (handCards[i] == null)
				{
					tempCard = deckCards[0];
					deckCards.Remove(tempCard);

					tempCard.GetComponent<CardBase>().newPos = handPos[i].position;
					tempCard.GetComponent<CardBase>().status = CardBase.Status.inHand;
					tempCard.GetComponent<CardBase>().index = i;

					handCards[i] = tempCard;

					break;
				}
			}
		}

		// 카드를 드로우 한 뒤 조합 상태를 확인
		UpdateCombination();
	}

	// 무덤의 카드를 덱으로 섞는다.
	IEnumerator ShuffleDeck()
	{
		GameObject tempCard;

		GameState tempState = gameState;
		gameState = GameState.ShuffleStart;

		while (tombCards.Count > 0)
		{
			// 무덤에서 랜덤카드 획득
			int random = Random.Range(0, tombCards.Count - 1);
			tempCard = tombCards[random];
			// 무덤에서 카드를 제거하고 덱에 추가
			tombCards.RemoveAt(random);
			deckCards.Add(tempCard);
			// 카드의 뉴포즈, 상태, 인덱스 업데이트
			tempCard.GetComponent<CardBase>().newPos = deckPos.position;
			tempCard.GetComponent<CardBase>().status = CardBase.Status.inDeck;
			tempCard.GetComponent<CardBase>().index = deckCards.IndexOf(tempCard);
			Debug.Log("tomb -> deck");
			yield return new WaitForSeconds(0.1f);
		}

		//gameState = tempState;
		gameState = GameState.ShuffleEnd;
	}

	// 카드의 위치가 다른 카드와 겹치는지, 묘지와 겹치는지 확인한다.
    public void CheckPlace(GameObject cardObject)
    {
        Vector3 currentCardPosition = cardObject.transform.position;
        currentCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다

        Vector3 tempCardPosition;
        float distance;
        
        // 공격 확인!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (currentCardPosition.y > 1 && cardObject.GetComponent<CardBase>().status == CardBase.Status.inField)
        {
            Debug.Log("Attack!!!!!!!!!!!!!!!!!!!!!");
            cardObject.transform.position = cardObject.GetComponent<CardBase>().newPos;
            StartCoroutine(AttackPhase());
            return;
        }

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
                    cardObject.transform.position = cardObject.GetComponent<CardBase>().newPos;
                    SwapCards(cardObject, tempCard); // 카드 교체
                    return;
                }
            }
            else
            {
                Debug.Log("Same Card(생략)");
            }
        }

        // 핸드의 다른 카드와 겹치는지 확인
        if (handCards[0] != cardObject)
        {
            tempCardPosition = handCards[0].transform.position;
            tempCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다
            distance = Vector3.Distance(currentCardPosition, tempCardPosition);
            if (distance < 1)
            {
                Debug.Log("Swap with hand");
                cardObject.transform.position = cardObject.GetComponent<CardBase>().newPos;
                SwapCards(cardObject, handCards[0]); // 카드 교체
                return;
            }
        }

        // 묘지와 겹치는지 확인
        tempCardPosition = tombPos.transform.position;
        tempCardPosition.z = 0; // 거리를 측정하기 위해 z값을 통일해준다
        distance = Vector3.Distance(currentCardPosition, tempCardPosition);
        if (distance < 1)
        {
            Debug.Log("Card to tomb");
            cardObject.transform.position = cardObject.GetComponent<CardBase>().newPos;
            CardBase.Status tempStatus = cardObject.GetComponent<CardBase>().status;
            CardToTomb(cardObject); // 카드 버림

			StartCoroutine(DrawCardFromDeck(tempStatus));

            return;
        }
    }

    IEnumerator AttackPhase()
    {
        // 카드 위치 초기화
        for (int i = 0; i < fieldCards.Length; i++)
        {
            fieldCards[i].transform.position = fieldPos[i].position;
            fieldCards[i].transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        // 전투!!!
        Vector3 tempVector = fieldPos [1].transform.position;
        tempVector.z -= 10;
        GameObject gameObject = Instantiate(Resources.Load("Prefabs/Effect/LeaderEffect") as GameObject, tempVector, Quaternion.identity); // should instantiate after load resources
		yield return new WaitForSeconds (0.5f);
		fieldCards [1].GetComponent<CardBase> ().ApplyLeaderEffect ();
        for (int i = 0; i < fieldCards.Length; i++)
        {
            gameState = GameState.CardAttacking;
            attackingCard = fieldCards[i];

			while (gameState != GameState.CardAttackFinish) {
				yield return new WaitForSeconds (0.1f);
			}
            //fieldCards[i].GetComponent<CardBase>().AttackMonster(monster, null);
		}
        gameState = GameState.Default;

        // 몬스터 공격!
        monster.GetComponent<MonsterBase>().AttackPlayer();

        // 턴 종료
        EndTurn();
        
        // 필드의 카드를 무덤으로!!!
        for (int i = 0; i < fieldCards.Length; i++)
        {
            CardToTomb(fieldCards[i]);
        }
        
        // 드로우!!!
        for (int i = 0; i < fieldCards.Length; i++)
        {
			StartCoroutine(DrawCardFromDeck(CardBase.Status.inField));
			yield return new WaitForSeconds (0.1f);
        }
    }

    public void CardToTomb(GameObject cardObject)
    {
        CardBase cardComponent = cardObject.GetComponent<CardBase>();

        if (cardComponent.status == CardBase.Status.inField)
        {
            Debug.Log("Field To Tomb");
            // 카드 소속 이동
            fieldCards[cardComponent.index] = null;
            tombCards.Add(cardObject);
            // 카드 정보 변경
            cardComponent.index = tombCards.IndexOf(cardObject); // 인덱스 업데이트
            cardComponent.status = CardBase.Status.inTomb; // 상태 업데이트
            Vector3 tempPosition = tombPos.position; // 카드의 뉴포즈를 무덤 밑으로 변경
            tempPosition.z++;
            cardComponent.newPos = tempPosition;
        }
        else if (cardComponent.status == CardBase.Status.inHand)
        {
            Debug.Log("Hand To Tomb");
            // 카드 소속 이동
            handCards[cardComponent.index] = null;
            tombCards.Add(cardObject);
            // 카드 정보 변경
            cardComponent.index = tombCards.IndexOf(cardObject); // 인덱스 업데이트
            cardComponent.status = CardBase.Status.inTomb; // 상태 업데이트
            Vector3 tempPosition = tombPos.position; // 카드의 뉴포즈를 무덤 밑으로 변경
            tempPosition.z++;
            cardComponent.newPos = tempPosition;
        }
    }


    public void SwapCards(GameObject card0, GameObject card1)
    {
        GameObject tempCard = Instantiate(card1) as GameObject;
        // 카드 1을 0의 자리로
        if (card0.GetComponent<CardBase>().status == CardBase.Status.inField)
        {
            //Debug.Log("hand to field");
            fieldCards[card0.GetComponent<CardBase>().index] = card1;
            //카드 0의 속성과 같아야 하는 부분(뉴포즈, 상태, 인덱스)
            fieldCards[card0.GetComponent<CardBase>().index].GetComponent<CardBase>().newPos = card0.GetComponent<CardBase>().newPos;
            fieldCards[card0.GetComponent<CardBase>().index].GetComponent<CardBase>().status = card0.GetComponent<CardBase>().status;
            fieldCards[card0.GetComponent<CardBase>().index].GetComponent<CardBase>().index = card0.GetComponent<CardBase>().index;
        }
        else
        {
            handCards[0] = card1;
            //카드 0의 속성과 같아야 하는 부분(뉴포즈, 상태, 인덱스)
            handCards[0].GetComponent<CardBase>().newPos = card0.GetComponent<CardBase>().newPos;
            handCards[0].GetComponent<CardBase>().status = card0.GetComponent<CardBase>().status;
            handCards[0].GetComponent<CardBase>().index = card0.GetComponent<CardBase>().index;
        }

        // 카드 0을 1의 자리로
        if (tempCard.GetComponent<CardBase>().status == CardBase.Status.inField) 
        {
            fieldCards[tempCard.GetComponent<CardBase>().index] = card0;
            //카드 1의 속성과 같아야 하는 부분(뉴포즈, 상태, 인덱스)
            fieldCards[tempCard.GetComponent<CardBase>().index].GetComponent<CardBase>().newPos = tempCard.GetComponent<CardBase>().newPos;
            fieldCards[tempCard.GetComponent<CardBase>().index].GetComponent<CardBase>().status = tempCard.GetComponent<CardBase>().status;
            fieldCards[tempCard.GetComponent<CardBase>().index].GetComponent<CardBase>().index = tempCard.GetComponent<CardBase>().index;
        }
        else
        {
            //Debug.Log("field to hand");
            handCards[0] = card0;
            //카드 1의 속성과 같아야 하는 부분(뉴포즈, 상태, 인덱스)
            handCards[0].GetComponent<CardBase>().newPos = tempCard.GetComponent<CardBase>().newPos;
            handCards[0].GetComponent<CardBase>().status = tempCard.GetComponent<CardBase>().status;
            handCards[0].GetComponent<CardBase>().index = tempCard.GetComponent<CardBase>().index;
        }
        //임시 카드 오브젝트 파괴
        Destroy(tempCard);

        // 카드의 위치를 바꾼 뒤의 조합 상태를 확인
        UpdateCombination();
    }

    // 필드에 있는 카드들의 조합 상태를 확인하고 필드, 핸드의 카드들의 수치를 업데이트한다.
    public void UpdateCombination()
    {
        // 필드에 카드가 3장 다 차있지 않을 경우 예외처리
        for(int i = 0; i < fieldCards.Length; i++)
        {
            if (fieldCards[i] == null)
                return;
        }
        // 핸드에 카드가 없을 경우 예외처리
        for (int i = 0; i < handCards.Length; i++)
        {
            if (handCards[i] == null)
                return;
        }
        
        // 필드의 카드들이 어떤 조합인지 확인 및 조합 배율 업데이트
        combination = baseCombination;
        int J = 0, Q = 0, K = 0, KNIGHT = 0, WIZARD = 0, PRIEST = 0; // 각각의 각인, 직업 숫자를 저장할 변수
        for (int i = 0; i < fieldCards.Length; i++) // // 각각의 각인, 직업 숫자를 파악
        {
            CardBase cardBase = fieldCards[i].GetComponent<CardBase>();
            if (cardBase.seal == CardBase.Seal.J)
                J++;
            if (cardBase.seal == CardBase.Seal.Q)
                Q++;
            if (cardBase.seal == CardBase.Seal.K)
                K++;
            if (cardBase.job == CardBase.Job.Knight)
                KNIGHT++;
            if (cardBase.job == CardBase.Job.Wizard)
                WIZARD++;
            if (cardBase.job == CardBase.Job.Priest)
                PRIEST++;
        }
        if (J == 2) // 파악한 갯수에 맞게 조합 배율 업데이트 => 각인
        {
            combination *= DOUBLE;
            Debug.Log("Double");
        }
        if (Q == 2)
        {
            combination *= DOUBLE;
            Debug.Log("Double");
        }
        if (K == 2)
        {
            combination *= DOUBLE;
            Debug.Log("Double");
        }
        if (J == 3)
        {
            combination *= TRIPLE;
            Debug.Log("Triple");
        }
        if (Q == 3)
        {
            combination *= TRIPLE;
            Debug.Log("Triple");
        }
        if (K == 3)
        {
            combination *= TRIPLE;
            Debug.Log("Triple");
        }
        if (J == 1 && Q == 1 && K == 1)
        {
            combination *= STRAIGHT;
            Debug.Log("Straight");
        }
        if (KNIGHT == 2) // 파악한 갯수에 맞게 조합 배율 업데이트 => 직업
        {
            combination *= DUO;
            Debug.Log("Duo");
        }
        if (WIZARD == 2)
        {
            combination *= DUO;
            Debug.Log("Duo");
        }
        if (PRIEST == 2)
        {
            combination *= DUO;
            Debug.Log("Duo");
        }
        if (KNIGHT == 3)
        {
            combination *= TRIO;
            Debug.Log("Trio");
        }
        if (WIZARD == 3)
        {
            combination *= TRIO;
            Debug.Log("Trio");
        }
        if (PRIEST == 3)
        {
            combination *= TRIO;
            Debug.Log("Trio");
        }
        if (KNIGHT == 1 && WIZARD == 1 && PRIEST == 1)
        {
            combination *= COLLABORATION;
            Debug.Log("Collaboration");
        }

        // 필드의 카드들에 조합 배율 적용
        for (int i = 0; i < fieldCards.Length; i++)
        {
            CardBase cardBase = fieldCards[i].GetComponent<CardBase>();

            cardBase.attackPoint = (int)(cardBase.baseAttackPoint * combination);
            cardBase.healPoint = (int)(cardBase.baseHealPoint * combination);
            cardBase.healthPoint = (int)(cardBase.baseHealthPoint * combination);
        }
        // 핸드의 카드에 조합 배율 제거
        for (int i = 0; i < handCards.Length; i++)
        {
            CardBase cardBase = handCards[i].GetComponent<CardBase>();

            cardBase.attackPoint = cardBase.baseAttackPoint;
            cardBase.healPoint = cardBase.baseHealPoint;
            cardBase.healthPoint = cardBase.baseHealthPoint;
        }
    }

    public void Attacked(int damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            healthPoint = 0;
            EndBattle();
        }
    }

    public void Healed(int healPoint)
    {
        healthPoint += healPoint;
        if (healthPoint > maxHealthPoint)
            healthPoint = maxHealthPoint;
    }

    void UpdateGame()
	{
		//UpdateBoard();
	}

	void EndTurn()
	{
		turnNumber += 1;

		OnNewTurn();
	}

	void OnNewTurn()
	{
		UpdateGame();
	}

    public void EndBattle()
    {
        //Time.timeScale = 0;
        sceneManager.GetComponent<ChangeScene>().ChangeSceneToReward();
    }
}