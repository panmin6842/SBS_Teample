using UnityEngine;

public class BowAttackManager : MonoBehaviour
{
    private Vector3 startPos;
    private float distance;

    [SerializeField] private string hitTag;

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

        damage1 = playerProfile.BasicATK(300);
        damage2 = playerProfile.BasicATK(200);
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
            if (playerAttack.through)
            {
                throughCount++;
                if (throughCount == 1)
                {

                }
                else if (throughCount == 2)
                {
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
