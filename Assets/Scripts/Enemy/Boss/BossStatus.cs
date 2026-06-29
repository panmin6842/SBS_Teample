using UnityEngine;

public class BossStatus : MonoBehaviour
{
    public float maxHp;
    public float curHp;
    public int atk;
    public float def;
    public float critical;

    private void Awake()
    {
        
    }

    public void GetDamage(float pDamage)
    {
        float damage = pDamage * (1 - def);
        curHp -= damage;
    }
}
