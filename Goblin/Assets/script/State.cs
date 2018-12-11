using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( menuName = "PluggableAI/State")]
public class State : ScriptableObject {

    public float speed;
    public AIAction[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.gray;
    
    public void UpdateState(StateController controller)
    {
        if(actions.Length > 0)
            DoActions(controller);
        CheckTransitions(controller);
    }
    public void SetupState(StateController controller)
    {
        SetSpeed(controller);
        SetAction(controller);
    }

    private void SetSpeed(StateController controller)
    {
        controller.navMeshAgent.speed = speed;
    }

    private void DoActions(StateController controller)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void SetAction(StateController controller)
    {
        if(actions != null && actions.Length > 0)
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Setup(controller);
            }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool deciisionSucceeded = transitions[i].decision.Decide(controller);

            if (deciisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
