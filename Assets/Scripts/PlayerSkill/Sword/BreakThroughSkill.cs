using System.Collections.Generic;
using UnityEngine;

public class BreakThroughSkill : MonoBehaviour
{
    [SerializeField] private HashSet<Collider> hitEnemies = new HashSet<Collider>();

    private GameObject player;
    private Rigidbody playerRid;
    private PlayerAttack playerAttack;
    private PlayerProfile playerProfile;

    private float rushSpeed = 10.0f;
    private float stopDist = 7.0f;

    private float damage;
    [SerializeField] private float damage1;
    [SerializeField] private float damage2;

    private Vector3 firstPos;
    private float dist;

    private bool attack = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRid = player.GetComponent<Rigidbody>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerProfile = player.GetComponent<PlayerProfile>();
        firstPos = transform.position;
        if (playerProfile != null)
        {
            playerProfile.UseMP(1);
            damage1 = playerProfile.ATK(400f);
            damage2 = playerProfile.ATK(150f);
            playerProfile.moveSpeed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        dist = Vector3.Distance(firstPos, transform.position);
        if (!attack)
        {
            playerRid.AddForce(transform.forward * rushSpeed
                    , ForceMode.Force);
        }

        if (dist >= stopDist)
        {
            Hit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Storage"))
        {
            Debug.Log("wall hit");
            Hit();
        }
    }
    public void OnPartHit(Collider enemy, int ColliderCount)
    {
        if (hitEnemies.Contains(enemy)) return;
        if (ColliderCount == 1)
        {
            damage = damage1;
        }
        else
        {
            damage = damage2;
        }

        hitEnemies.Add(enemy);
        //공격 및 넉백 제작
        Debug.Log("스킬 : 일점 돌파" + enemy.gameObject.name + "을(를) 공격했습니다!" + "데미지 : " + damage);
        if (playerProfile.BloodHeal)
            playerProfile.BloodHealHp(10, damage);
    }

    private void Hit()
    {
        playerRid.linearVelocity = Vector3.zero;
        attack = true;
        playerProfile.ChangeMoveSpeed(1);
        playerProfile.SkillStart = false;
        Destroy(this.gameObject);
    }
}
