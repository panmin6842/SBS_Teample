using System.Threading;
using Cysharp.Threading.Tasks;

public interface IAttackBehavior
{
    UniTask Attack(int number, CancellationToken token);
    bool GetIsMultiProjectileAttack(int randomNum);
}
