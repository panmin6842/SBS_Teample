using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] int damage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(damage);
            other.GetComponent<PlayerProfile>().GetDamage(damage);
        }
    }
}
