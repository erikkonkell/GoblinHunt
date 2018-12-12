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

    public override void Setup(StateController controller)
    {
    }

    private void Attack(StateController controller)
    {
        controller.isRangeAttacking = true;
        controller.RotateTowards(controller.chaseTarget);
        
    }
}
