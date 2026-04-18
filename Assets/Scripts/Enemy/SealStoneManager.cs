using UnityEngine;

public class SealStoneManager : MonoBehaviour
{
    [SerializeField] private float hp;
    private void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damage(float damage)
    {
        hp -= damage;
    }
}
