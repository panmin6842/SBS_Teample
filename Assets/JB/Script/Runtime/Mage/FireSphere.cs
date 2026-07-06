using UnityEngine;

public class FireSphere : MonoBehaviour
{
    private Vector3 destination;
    private SphereCollider explosionRange;
    private EnemyBase enemyBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = GameObject.FindWithTag("Player").transform.position;
        enemyBase = this.transform.parent.GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate((destination - transform.position).normalized);
        if (destination.sqrMagnitude == this.transform.position.sqrMagnitude)
            Explosion();
    }

    private void Explosion()
    {
        SphereCollider explosion = null;
        explosion = Instantiate(explosionRange, this.transform);
        if (explosion != null && explosion.GetComponent<Collision>().gameObject.CompareTag("Player"))
        {
            explosion.GetComponent<Collision>().gameObject.GetComponent<PlayerProfile>().GetDamage((int)(enemyBase.EnemyInfo.AttackPower * 0.8f));
        }
        Destroy(explosion);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.GetComponent<PlayerProfile>().GetDamage(enemyBase.EnemyInfo.AttackPower * 2);
        Explosion();
        Destroy(col.gameObject);
    }
}
