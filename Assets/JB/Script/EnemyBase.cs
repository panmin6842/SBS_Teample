using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float attackRange = 10.0f;
    [SerializeField] protected float detectionRange = 20.0f;

    protected virtual Vector3 DetectPlayer()
    {
        Debug.Log("Enemy detects the player!");
        return Vector3.zero;
    }

    protected virtual void PrepareForAttack()
    {
        Debug.Log("Enemy sets up for an attack!");
    }

    protected virtual void Attack()
    {
        Debug.Log("Enemy attacks!");
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy takes " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Enemy dies!");
        Destroy(this.gameObject);
    }

    protected virtual async void CheckDistanceToPlayer()
    {
        while (true)
        {
            Vector3 playerPosition = DetectPlayer();
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            if (distanceToPlayer <= attackRange)
            {
                PrepareForAttack();
                Attack();
            }
            else if (distanceToPlayer <= detectionRange)
            {
                Debug.Log("Enemy is chasing the player!");
                // Implement chasing logic here
            }
            else
            {
                Debug.Log("Enemy is idle.");
                // Implement idle behavior here
            }

            await System.Threading.Tasks.Task.Delay(1000); // Check every second
        }
    }
}
