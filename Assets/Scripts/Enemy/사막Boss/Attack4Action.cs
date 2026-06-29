using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack4", story: "try [attack4] player", category: "Action", id: "53c3586f326391b431282290f7024fd6")]
public partial class Attack4Action : Action
{
    [SerializeReference] public BlackboardVariable<BossAttack> Attack4;

    protected override Status OnStart()
    {
        Attack4.Value.Attack4();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!Attack4.Value.isAttacking)
            return Status.Success;

        return Status.Running;
    }
}

