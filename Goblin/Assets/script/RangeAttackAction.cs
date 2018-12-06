using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RangeAttack")]
public class RangeAttackAction : AIAction
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }
    private void Attack(StateController controller)
    {
        controller.isRangeAttacking = true;
        controller.navMeshAgent.speed = 0;
        controller.RotateTowards(controller.chaseTarget);
        
    }
}
