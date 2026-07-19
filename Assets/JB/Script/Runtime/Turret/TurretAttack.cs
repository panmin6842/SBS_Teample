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
    }

    public void FixedShotTypeB()
    {
        Debug.Log("TypeB Shot");
        GameObject obj = GameObject.Instantiate(turretPrefabs[0], enemy.transform.position, enemy.transform.rotation);
        obj.transform.rotation = obj.transform.rotation * Quaternion.Euler(0, 45, 0);
    }

    public void SnipingShot()
    {
        Debug.Log("Sniping Shot");
        GameObject obj = GameObject.Instantiate(turretPrefabs[1], enemy.transform.position, enemy.transform.rotation);
    }

    public void Attack(int randomAttack)
    {
        switch (randomAttack)
        {
            case 0:
                FixedShotTypeA();
                break;
            case 1:
                FixedShotTypeA();
                break;
            case 2:
                FixedShotTypeB();
                break;
            case 3:
                FixedShotTypeB();
                break;
            case 4:
                SnipingShot();
                break;
        }
    }
}
