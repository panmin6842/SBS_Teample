using System.Collections;
using UnityEngine;

public class StampAttackManager : MonoBehaviour
{
    private Vector3 startPos;
    private float distance;
    private bool bomb = false;

    private SpriteRenderer sr;
    private PlayerAttack playerAttack;
    private PlayerProfile playerProfile;

    private float bombScale = 0.5f;
    private float bombStartScale;
    private float damage1;
    private float damage2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        playerProfile = GameObject.Find("Player").GetComponent<PlayerProfile>();

        bombStartScale = bombScale;

        if (playerProfile != null)
        {
            if (!playerAttack.stampPassiveSkill1)
            {
                damage1 = playerProfile.BasicATK(250);
                damage2 = playerProfile.BasicATK(180);
            }
            else if (playerAttack.stampPassiveSkill1)
            {
                damage1 = playerProfile.BasicATK(280);
                damage2 = playerProfile.BasicATK(220);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bomb)
        {
            transform.position += transform.up * playerAttack.power * Time.deltaTime;

            distance = Vector3.Distance(startPos, transform.position);

            if (distance > playerAttack.shotDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    public void IncreasedColliderSize(float size)
    {
        bombScale = bombStartScale + size;
    }

    private Collider _other;
    private void OnTriggerEnter(Collider other)
    {
        //임시
        if (other.tag == "Enemy")
        {
            Debug.Log("스탬프 기본 직격 공격" + other.gameObject.name + "을(를) 공격했습니다!" + "damage1 = " + damage1);
            bomb = true;
            _other = other;
            StartCoroutine(BombDestroy(_other));
        }
    }

    IEnumerator BombDestroy(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        Color32 orange = new Color32(255, 160, 0, 255);
        GetComponent<SpriteRenderer>().color = orange;
        if ((playerAttack.stampSkill6 && !playerAttack.stampPassiveSkill1)
        || (!playerAttack.stampSkill6 && playerAttack.stampPassiveSkill1))
        {
            IncreasedColliderSize(0.5f);
        }
        else if (playerAttack.stampSkill6 && playerAttack.stampPassiveSkill1)
        {
            IncreasedColliderSize(1.0f);
        }
        transform.localScale = new Vector3(bombScale, bombScale, bombScale);
        Debug.Log("스탬프 기본 폭발 공격" + other.gameObject.name + "을(를) 공격했습니다!" + "damage2 = " + damage2);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
