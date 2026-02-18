using UnityEngine;

public class BlowSkill : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRid;
    private PlayerAttack playerAttack;
    private PlayerProfile playerProfile;

    private float rushSpeed = 15.0f;
    private float stopDist = 10.0f;

    private float damage;

    private Vector3 firstPos;
    private float dist;

    private bool rush = false;
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
            playerProfile.UseMP(2);
            damage = playerProfile.ATK(1900f);
            playerProfile.NoDamage = true;
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
            if (!rush)
            {
                transform.rotation = Quaternion.Euler(0, playerAttack.AttackPos.transform.localRotation.eulerAngles.y, 0);
                Invoke("RushStart", 2);
            }
            else //돌격
            {
                Debug.Log("rushStart");

                playerRid.AddForce(transform.forward * rushSpeed
                    , ForceMode.Force);
            }
        }

        if (dist >= stopDist)
        {
            Hit();
        }
    }

    private void RushStart()
    {
        rush = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("스킬 : 회심의 일격" + other.gameObject.name + "을(를) 공격했습니다!");
            Hit();
        }
        if (other.CompareTag("Wall"))
        {
            Hit();
        }

    }

    private void Hit()
    {
        playerRid.linearVelocity = Vector3.zero;
        playerProfile.NoDamage = false;
        attack = true;
        playerProfile.ChangeMoveSpeed(1);
        Destroy(this.gameObject);
    }
}
