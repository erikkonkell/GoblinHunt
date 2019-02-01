using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/UnderAttack")]
public class UnderAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return UnderAttack(controller);
    }
    private bool UnderAttack(StateController controller)
    {
        if(controller.LostHealth())
        {
            controller.currentHealthLastFrame = controller.GetComponent<vp_DamageHandler>().CurrentHealth;
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
