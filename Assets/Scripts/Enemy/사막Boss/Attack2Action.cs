using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack2", story: "try [attack2] player", category: "Action", id: "2a1df13ce582f26ab530ea9b53c826f0")]
public partial class Attack2Action : Action
{
    [SerializeReference] public BlackboardVariable<BossAttack> Attack2;

    protected override Status OnStart()
    {
        Attack2.Value.Attack2();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Attack2.Value.isAttacking)
            return Status.Success;

        return Status.Running;
    }
}

