using UnityEngine;

public class BarrierSkill : MonoBehaviour
{
    private PlayerProfile playerProfile;
    private GameObject player;
    [SerializeField] private GameObject shockWave;

    [SerializeField] private float barrierHp;

    private float stopTime = 5;
    private float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerProfile = player.GetComponent<PlayerProfile>();

        if (playerProfile != null)
        {
            playerProfile.UseMP(1);
            playerProfile.Barrier = true;
            barrierHp = playerProfile.MaxHp * 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        time += Time.deltaTime;
        if (barrierHp <= 0 || time > stopTime)
        {
            Instantiate(shockWave, transform.position, shockWave.transform.rotation);
            playerProfile.SkillStart = false;
            playerProfile.Barrier = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //공격당하는걸 임시로 적에 닿았을경우로 함
        if (other.CompareTag("Enemy"))
        {
            barrierHp -= 5;
        }
    }
}
