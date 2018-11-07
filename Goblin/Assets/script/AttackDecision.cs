using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Attack")]
public class AttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return Attack(controller);
    }

    private bool Attack(StateController controller)
    {
        return Vector3.Distance(controller.transform.position, controller.chaseTarget.position) < controller.enemyStats.attackRange;
    }
}
