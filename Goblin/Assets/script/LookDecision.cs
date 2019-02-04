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
       // Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, controller.currentState.sceneGizmoColor);

        //if(Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward.normalized, out hit, controller.enemyStats.lookRange)
        //    && hit.collider.CompareTag("Player"))
        //{
        //    controller.chaseTarget = hit.transform;
        //    return true;
        //}
        Vector3 direction = controller.chaseTarget.transform.position;
        direction.y += 1;
        direction -= controller.eyes.position;
        float angle = Vector3.Angle(direction, controller.transform.forward);
        Debug.DrawRay(controller.eyes.position, direction.normalized * controller.enemyStats.lookRange, controller.currentState.sceneGizmoColor);
        if(angle < controller.enemyStats.fieldOfViewAngle *0.5f)
        {
            if (controller.chaseTarget == null)
                controller.chaseTarget = FindObjectOfType<vp_FPController>().transform;
            if (willIgnoreAI)
            {
                int layerMask = 1 << 10;
                layerMask = ~layerMask;
                if (Physics.Raycast(controller.eyes.position, direction.normalized, out hit, controller.enemyStats.lookRange,layerMask))
                {
                    if (hit.collider.gameObject.transform.parent.gameObject.tag == controller.chaseTarget.tag)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
            {
                int layerMask = ~0;
                if (Physics.Raycast(controller.eyes.position, direction.normalized, out hit, controller.enemyStats.lookRange, layerMask))
                {
                    if (hit.collider.gameObject.transform.parent.gameObject.tag == controller.chaseTarget.tag)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }
        else
        {
            return false;
        }
    }
}
