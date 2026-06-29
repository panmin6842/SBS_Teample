using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack1", story: "try [attack] player", category: "Action", id: "05da41acf854db3a8b8e8dbc1061d630")]
public partial class Attack1Action : Action
{
    [SerializeReference] public BlackboardVariable<BossAttack> Attack;

    protected override Status OnStart()
    {
        Attack.Value.Attack1();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Attack.Value.isAttacking)
            return Status.Success;

        return Status.Running;
    }
}

