using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AttackState : StateBase
{
    public override UniTask Enter(CancellationToken token)
    {
        Debug.Log("공격 상태로 진입");
        return UniTask.CompletedTask;
    }

    public override UniTask Tick(CancellationToken token)
    {
        Debug.Log("공격 상태에서 행동 중");
        return UniTask.CompletedTask;
    }

    public override UniTask Exit(CancellationToken token)
    {
        Debug.Log("공격 상태에서 나감");
        return UniTask.CompletedTask;
    }

}
