using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MageAttack : IAttackBehavior
{
    private GameObject[] magePrefabs;
    private EnemyBase enemy;


    public bool GetIsMultiProjectileAttack(int randomAttack) => false;
    
    public MageAttack(GameObject[] magePrefabs, EnemyBase enemy)
    {
        this.magePrefabs = magePrefabs;
        this.enemy = enemy;
    }

    private void MPBullet()
    {
        GameObject magic = GameObject.Instantiate(magePrefabs[0], enemy.transform.position, enemy.transform.rotation);
    }

    private void Fireball()
    {
        GameObject magic = GameObject.Instantiate(magePrefabs[1], enemy.transform.position, enemy.transform.rotation);
        magic.transform.parent = enemy.transform;
    }

    public async UniTask Attack(int randomAttack, CancellationToken token)
    {
        Debug.Log("Mage Attack!");
        switch (randomAttack)
        {
            case 3:
                Fireball();
                break;
            default:
                MPBullet();
                break;
        }
        await UniTask.CompletedTask;
    }
}
