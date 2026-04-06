using UnityEngine;

public class Attack5Manager : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float rotateSpeed = 80f;

    private float damage;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();

        if (playerProfile != null)
        {
            damage = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        if (timer > 5)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("스킬 : 5" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
        }
    }
}
