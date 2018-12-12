using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : AIAction
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    public void Chase(StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.isStopped = false;
    }

    public override void Setup(StateController controller)
    {

    }
}

