using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * 7f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerProfile>().GetDamage(this.GetComponentInParent<EnemyBase>().EnemyInfo.AttackPower);
        }
        Destroy(this.gameObject);
    }
}
