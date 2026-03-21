using UnityEngine;

public class BowAttackManager : MonoBehaviour
{
    private Vector3 startPos;
    private float distance;

    [SerializeField] private string hitTag;
    [SerializeField] private GameObject bowExplosionObj;

    private PlayerAttack playerAttack;
    private PlayerProfile playerProfile;

    private float damage1;
    private float damage2;
    private int throughCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        playerProfile = GameObject.Find("Player").GetComponent<PlayerProfile>();
        startPos = transform.position;

        if (!playerAttack.bowPassiveSkill3)
        {
            damage1 = playerProfile.BasicATK(300);
            damage2 = playerProfile.BasicATK(200);
        }
        else if (playerAttack.bowPassiveSkill3)
        {
            damage1 = playerProfile.BasicATK(350);
            damage2 = playerProfile.BasicATK(230);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * playerAttack.power * Time.deltaTime;

        distance = Vector3.Distance(startPos, transform.position);

        if (distance > playerAttack.shotDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (playerAttack.bowExplosion)
            {
                int number = Random.Range(1, 101);
                Debug.Log("BowExplosionRandomNumber : " + number);
                if (number > 0 && number <= 30)
                {
                    Instantiate(bowExplosionObj, transform.position, transform.rotation);
                }
            }

            if (playerAttack.through)
            {
                throughCount++;
                if (throughCount == 1)
                {
                    Debug.Log("궁수 기본 공격" + other.gameObject.name + "을(를) 공격했습니다!" + "damage1 = " + damage1);
                }
                else if (throughCount == 2)
                {
                    Debug.Log("궁수 기본 공격" + other.gameObject.name + "을(를) 공격했습니다!" + "damage2 = " + damage2);
                    Destroy(gameObject);
                }
            }
            else
                Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
