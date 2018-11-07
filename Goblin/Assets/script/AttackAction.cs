using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : AIAction
{
    private StateController StateController;
    public override void Act(StateController controller)
    {
        Attack(controller);
        StateController = controller;
    }
    private void Attack(StateController controller)
    {
        controller.isAttacking = true;
        RaycastHit hit;
        if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.attackRange)
            && hit.collider.CompareTag("Player"))
        {
            if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
            {
                controller.chaseTarget.gameObject.GetComponentInParent<vp_DamageHandler>().Damage(controller.enemyStats.attackDamage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(StateController.transform.position, StateController.enemyStats.attackRange);
    }


}