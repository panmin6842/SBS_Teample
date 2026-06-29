using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack3", story: "try [attack3] player", category: "Action", id: "c2d5bdf8a113c9eafbad52e54e3209ee")]
public partial class Attack3Action : Action
{
    [SerializeReference] public BlackboardVariable<BossAttack> Attack3;

    protected override Status OnStart()
    {
        Attack3.Value.Attack3();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Attack3.Value.isAttacking)
            return Status.Success;

        return Status.Running;
    }
}

