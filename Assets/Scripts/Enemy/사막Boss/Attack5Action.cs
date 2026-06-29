using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack5", story: "try [attack5] player", category: "Action", id: "244eb6027dd3ad09a233a2056aac7608")]
public partial class Attack5Action : Action
{
    [SerializeReference] public BlackboardVariable<BossAttack> Attack5;

    protected override Status OnStart()
    {
        Attack5.Value.Attack5();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Attack5.Value.isAttacking)
            return Status.Success;

        return Status.Running;
    }
}

