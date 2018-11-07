using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    [SerializeField]private List<Transform> wayPointList;
    [SerializeField] private List<StateController> enemyList;

    private void Start()
    {
        foreach (StateController stateController in enemyList)
        {
            stateController.SetupAI(true, wayPointList);
        }
    }
}
