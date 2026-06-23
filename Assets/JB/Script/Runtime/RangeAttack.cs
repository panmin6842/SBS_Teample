using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProjectileTypePattern : IAttackBehavior
{
    private GameObject projectilePrefab;
    private EnemyBase enemy;

    public ProjectileTypePattern(GameObject projectilePrefab, EnemyBase enemy)
    {
        this.projectilePrefab = projectilePrefab;
        this.enemy = enemy;
    }
    
    public void SingleProjectile()
    {
        Debug.Log("Single Projectile Attack!");
        GameObject.Instantiate(projectilePrefab, enemy.GetComponentInChildren<Transform>().position, enemy.GetComponentInChildren<Transform>().rotation);
    }
    
    public void SpreadProjectile()
    {
        Debug.Log("Spread Projectile Attack!");
    }

    
    public async UniTaskVoid SnipingProjectile()
    {
        Debug.Log("Sniping Projectile Attack!");
        for (int i = 0; i < 3; i++)
        {
            GameObject.Instantiate(projectilePrefab, enemy.GetComponentInChildren<Transform>().position, enemy.GetComponentInChildren<Transform>().rotation);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
    }

    public void Attack()
    {

        int randomAttack = UnityEngine.Random.Range(0, 3); // 0, 1, 2 중 하나를 랜덤으로 선택
        switch (randomAttack)
        {
            case 0:
                SingleProjectile();
                break;
            case 1:
                SpreadProjectile();
                break;
            case 2:
                SnipingProjectile().Forget();
                break;
        }
    }
}
