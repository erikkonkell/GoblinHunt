using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Lost Target")]
public class LostTargetDecision : Decision {
    [SerializeField] float memory;
    public override bool Decide(StateController controller)
    {
        return LostTarget(controller);
    }

    public bool LostTarget(StateController controller)
    {
        return controller.CheckIfCountDownElapsed(memory);
    }
}
