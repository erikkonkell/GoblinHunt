using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : ScriptableObject {

    public abstract void Act(StateController controller);
    public abstract void Setup(StateController controller);
}
