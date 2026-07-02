using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProjectileTypePattern : IAttackBehavior
{
    private GameObject[] projectilePrefab;
    private EnemyBase enemy;

    public ProjectileTypePattern(GameObject[] projectilePrefab, EnemyBase enemy)
    {
        this.projectilePrefab = projectilePrefab;
        this.enemy = enemy;
    }

    public void SingleProjectile()
    {
        Debug.Log("Single Projectile Attack!");
        GameObject projectile = GameObject.Instantiate(projectilePrefab[0], enemy.GetComponentInChildren<Transform>().position, enemy.GetComponentInChildren<Transform>().rotation);
        projectile.GetComponent<Transform>().parent = enemy.transform;
    }
    
    public void SpreadProjectile()
    {
        Debug.Log("Spread Projectile Attack!");
        GameObject projectile = GameObject.Instantiate(projectilePrefab[1], enemy.GetComponentInChildren<Transform>().position, enemy.GetComponentInChildren<Transform>().rotation);
        projectile.GetComponent<Transform>().parent = enemy.transform;
    }

    
    public async UniTaskVoid SnipingProjectile()
    {
        Debug.Log("Sniping Projectile Attack!");
        for (int i = 0; i < 3; i++)
        {
            GameObject projectile = GameObject.Instantiate(projectilePrefab[0], enemy.GetComponentInChildren<Transform>().position, enemy.GetComponentInChildren<Transform>().rotation);
            projectile.GetComponent<Transform>().parent = enemy.transform;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
    }

    public void Attack(int randomAttack)
    {
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
