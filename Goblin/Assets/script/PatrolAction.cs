using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : AIAction {

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    public override void Setup(StateController controller)
    {
        
    }

    private void Patrol(StateController controller)
    {
        controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        controller.navMeshAgent.isStopped = false;

        if(controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.nextWayPoint = Random.Range(0, controller.wayPointList.Count - 1);
        }
    }
}