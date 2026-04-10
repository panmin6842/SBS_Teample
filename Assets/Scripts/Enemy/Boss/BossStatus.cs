using UnityEngine;

public class BossStatus : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float atk;
    [SerializeField] private float def;

    public void GetDamage(float pDamage)
    {
        float damage = pDamage; //방어력 포함된 데미지 넣기
        hp -= damage;
    }
}
