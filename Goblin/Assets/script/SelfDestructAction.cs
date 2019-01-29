using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SelfDestruct")]
public class SelfDestructAction : AIAction
{
    public override void Act(StateController controller)
    {
        controller.GetComponent<vp_DamageHandler>().Damage(Mathf.Infinity);
    }

    public override void Setup(StateController controller)
    {
        
    }
}
