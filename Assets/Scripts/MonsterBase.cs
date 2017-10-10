using UnityEngine;

[System.Serializable]
public class MonsterBase : MonoBehaviour
{
    public string monsterName;
    public string discription;

    public int healthPoint;
    public int attackPoint;


    public TextMesh healthText;

    public Vector3 homePosition;

    // Use this for initialization
    void Start()
    {
        healthText.text = healthPoint.ToString();
    }

    private void Update()
    {
        healthText.text = healthPoint.ToString();
    }

    private void FixedUpdate()
    {
        healthText.text = healthPoint.ToString();
        transform.position = Vector3.Lerp(transform.position, homePosition, Time.deltaTime * 3);
    }
}
