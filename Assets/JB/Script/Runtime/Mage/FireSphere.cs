using UnityEngine;

public class FireSphere : MonoBehaviour
{
    [SerializeField] private Vector3 destination;
    private EnemyBase enemyBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = GameObject.FindWithTag("Player").transform.position;
        enemyBase = this.transform.parent.GetComponent<EnemyBase>();
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(destination);
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, Time.deltaTime * 4f);
        if (Vector3.Distance(destination, this.transform.position) < 0.1f)
            Explosion();
    }

    private void Explosion()
    {
        // SphereCollider의 중심점과 반지름을 사용해 범위 안의 모든 Collider 검출                 
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 2f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("<color=orange>Player Hit</color>");
                PlayerProfile player = hitCollider.GetComponent<PlayerProfile>();
                if (player != null)
                {
                    player.GetDamage(enemyBase.EnemyInfo.AttackPower * 0.8f);
                }
            }
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.GetComponent<PlayerProfile>().GetDamage(enemyBase.EnemyInfo.AttackPower * 2);
        Explosion();
        Destroy(col.gameObject);
    }
}
