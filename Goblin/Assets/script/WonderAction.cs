using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Wonder")]
public class WonderAction : AIAction
{
    public override void Act(StateController controller)
    {
        Wonder(controller);
    }

    private void Wonder( StateController controller)
    {
        if(controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.navMeshAgent.destination = controller.RandomNavSphere(controller.transform.position, controller.enemyStats.wonderRange);
        }
    }
}
