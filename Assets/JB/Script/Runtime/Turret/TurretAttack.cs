using UnityEngine;

public class TurretAttack : IAttackBehavior
{
    private GameObject[] turretPrefabs;
    private EnemyBase enemy;

    public bool GetIsMultiProjectileAttack(int randomNum) => false;

    public TurretAttack(GameObject[] turretPrefabs, EnemyBase enemy)
    {
        this.turretPrefabs = turretPrefabs;
        this.enemy = enemy;
    }

    public void FixedShotTypeA()
    {
        Debug.Log("TypeA Shot");
        GameObject obj = GameObject.Instantiate(turretPrefabs[0], enemy.transform.position, enemy.transform.rotation);
        obj.transform.parent = enemy.transform;
    }

    public void FixedShotTypeB()
    {
        Debug.Log("TypeB Shot");
    }

    public void Attack(int randomAttack)
    {
        switch (randomAttack)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
