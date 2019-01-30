using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<StateController> spawnAI;
    public List<Transform> wayPoints;

    public Transform aiStartPos;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == vp_Layer.Trigger && collision.gameObject.GetComponentInParent<vp_DamageHandler>() != null)
        {
            foreach (StateController controller in spawnAI)
            {
                StateController sc = Instantiate<StateController>(controller);
                sc.SetupAI(true, wayPoints);
                sc.transform.position = aiStartPos.position;
            }
            Debug.Log("Enemies has spawned!!");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == vp_Layer.Trigger && other.gameObject.GetComponentInParent<vp_DamageHandler>() != null)
        {
            foreach (StateController controller in spawnAI)
            {
                StateController sc = Instantiate<StateController>(controller);
                sc.SetupAI(true,wayPoints);
                sc.transform.position = aiStartPos.position;
            }
            Debug.Log("Enemies has spawned!!");

        }
    }
}
