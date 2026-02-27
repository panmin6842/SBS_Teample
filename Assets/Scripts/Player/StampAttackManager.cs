using System.Collections;
using UnityEngine;

public class StampAttackManager : MonoBehaviour
{
    Vector3 startPos;
    float distance;
    bool bomb = false;

    SpriteRenderer sr;
    private PlayerAttack playerAttack;

    float bombScale = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
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

    private void OnTriggerEnter(Collider other)
    {
        //юс╫ц
        if (other.tag == "Enemy")
        {
            bomb = true;
            transform.localScale = new Vector3(bombScale, bombScale, bombScale);
            //Color orange = new Color(255f / 255f, 143f / 255f, 0);
            Color orange = new Color(255f, 160f, 0);
            GetComponent<SpriteRenderer>().color = orange;
            StartCoroutine(BombDestroy());
        }
    }

    IEnumerator BombDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
