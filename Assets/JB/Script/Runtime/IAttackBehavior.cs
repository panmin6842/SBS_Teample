using Cysharp.Threading.Tasks;

public interface IAttackBehavior
{
    void Attack(int number);
    bool GetIsMultiProjectileAttack(int number);
}
