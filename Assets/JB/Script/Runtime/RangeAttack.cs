using UnityEngine;

public class ProjectileTypePattern : IAttackBehavior
{
    private GameObject projectilePrefab;

    public ProjectileTypePattern(GameObject projectilePrefab)
    {
        this.projectilePrefab = projectilePrefab;
    }
    
    public void SingleProjectile()
    {
        Debug.Log("Single Projectile Attack!");
    }
    
    public void SpreadProjectile()
    {
        Debug.Log("Spread Projectile Attack!");
    }

    
    public void SnipingProjectile()
    {
        Debug.Log("Sniping Projectile Attack!");
    }

    public void Attack()
    {

        int randomAttack = Random.Range(0, 3); // 0, 1, 2 중 하나를 랜덤으로 선택
        switch (randomAttack)
        {
            case 0:
                SingleProjectile();
                break;
            case 1:
                SpreadProjectile();
                break;
            case 2:
                SnipingProjectile();
                break;
        }
    }
}
