using TMPro;
using UnityEngine;

public class EnemyTest : PlayerState
{
    private float enemyHp = 100;

    [SerializeField] TextMeshProUGUI enemyHpText;
    [SerializeField] GameObject item;
    [SerializeField] GameObject potal;

    [SerializeField] GameObject attack;

    float dist;

    GameObject player;

    float attackTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemyHpText.text = "EnemyHp : " + enemyHp;

        //Debug.Log(dist);

        dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist < 15)
        {
            if (attackTime > 3)
            {
                Instantiate(attack, transform.position, Quaternion.identity);
                attackTime = 0;
            }
            else
            {
                attackTime += Time.deltaTime;
            }
        }
        else
        {
            attackTime = 0;
        }

        if (enemyHp <= 0)
        {
            enemyHp = 0;
            item.SetActive(true);
            potal.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SwordBasicAttack" || other.tag == "StampBasicAttack" || other.tag == "ArrowBasicAttack")
        {
            enemyHp -= atk;
        }
    }
}
