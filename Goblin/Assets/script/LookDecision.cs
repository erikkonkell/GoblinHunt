using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {
    public bool willIgnoreAI;
    public override bool Decide(StateController controller)
    {
        bool targetVisivle = Look(controller);

        return targetVisivle;
    }
    private bool Look(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, controller.currentState.sceneGizmoColor);

        //if(Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward.normalized, out hit, controller.enemyStats.lookRange)
        //    && hit.collider.CompareTag("Player"))
        //{
        //    controller.chaseTarget = hit.transform;
        //    return true;
        //}
        if (willIgnoreAI)
        {
            int layerMask = 1 << 10;
            layerMask = ~layerMask;
            if(Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit,controller.enemyStats.lookRange,layerMask)
                && hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange)
                && hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //normal raycast fail when player moves keep switching states

        //if(willIgnoreAI)
        //{
        //    if(Physics.Raycast(controller.eyes.position,controller.eyes.TransformDirection(Vector3.forward),out hit, controller.enemyStats.lookRange,layerMask)
        //        && hit.collider.CompareTag("Player"))
        //    {
        //        controller.chaseTarget = hit.transform;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    if (Physics.Raycast(controller.eyes.position, controller.eyes.TransformDirection(Vector3.forward), out hit, controller.enemyStats.lookRange)
        //        && hit.collider.CompareTag("Player"))
        //    {
        //        controller.chaseTarget = hit.transform;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
