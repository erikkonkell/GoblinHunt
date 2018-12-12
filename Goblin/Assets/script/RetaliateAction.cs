using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetaliateAction : AIAction {
    public override void Act(StateController controller)
    {
    }

    public override void Setup(StateController controller)
    {
        controller.navMeshAgent.SetDestination(controller.chaseTarget.position);
    }
}
