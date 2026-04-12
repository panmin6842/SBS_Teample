using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    MonsterSpawnManager monsterSpawnManager;
    bool isMoving = false;
    [SerializeField] MonsterStatData MonsterData;
    [SerializeField] GameObject SectorAttackAoE;
    [SerializeField] GameObject SectorAttackEffect;
    [SerializeField] GameObject ThrustAttackAoE;
    [SerializeField] GameObject ThrustAttackEffect;

    float attackTimer = 0f;
    bool isAttacking;

    GameObject Player;

    private void Awake()
    {
        monsterSpawnManager = MonsterSpawnManager.instance;
        MonsterData.CurHP = MonsterData.MaxHP;
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (MonsterData.CurHP <= 0)
        {
            monsterSpawnManager.MonsterDead();
            Destroy(gameObject);
        }

        if (!isMoving)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        isMoving = true;
        int randomAttack = Random.Range(0, 10);

        if (Vector3.Distance(transform.localPosition, Player.transform.localPosition) <= MonsterData.AttackRange && !isAttacking && attackTimer >= MonsterData.AttackDelay)
        {
            isAttacking = true;
            if (randomAttack >= 0 && randomAttack <= 2)
            {
                StartCoroutine(ThrustAttack());
            }
            else
            {
                StartCoroutine(SectorAttack());
            }

            yield return new WaitForSeconds(0f);
        }
        
        if (!isAttacking && Vector3.Distance(transform.localPosition, Player.transform.localPosition) > MonsterData.AttackRange)
        {
            MoveToPlayer();
            attackTimer += Time.deltaTime;
        }

        isMoving = false;
    }

    void MoveToPlayer()
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = rot;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * MonsterData.MoveSpeed);
    }

    IEnumerator ThrustAttack()
    {
        Sprite sprite = ThrustAttackEffect.GetComponent<SpriteRenderer>().sprite;

        ThrustAttackEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ThrustAttackEffect.SetActive(false);
        ThrustAttackAoE.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ThrustAttackAoE.SetActive(false);

        isAttacking = false;
        attackTimer = 0f;
    }

    IEnumerator SectorAttack()
    {
        Sprite sprite = SectorAttackEffect.GetComponent<SpriteRenderer>().sprite;

        SectorAttackEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        SectorAttackEffect.SetActive(false);
        SectorAttackAoE.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SectorAttackAoE.SetActive(false);

        isAttacking = false;
        attackTimer = 0f;
    }

    IEnumerator HitByPlayer()
    {
        MonsterData.CurHP -= 10f;

        yield return new WaitForSeconds(0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SwordBasicAttack"))
        {
            StartCoroutine(HitByPlayer());
        }
    }
}
