using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( menuName = "PluggableAI/State")]
public class State : ScriptableObject {

    public float speed;
    public bool needAllTransitions = false;
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
        if(!needAllTransitions)
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
        //if you need all transitions to be true to move to next state
        //all transitions need to have remainInState on false
        else
        {
            bool change = true;
            for (int i = 0; i < transitions.Length; i++)
            {
                if (!transitions[i].decision.Decide(controller))
                {
                    change = false;
                }
            }
            if(change)
                controller.TransitionToState(transitions[0].trueState);
            else
                controller.TransitionToState(transitions[0].falseState);
        }
    }
}
