using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with trigger (on collision)");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with trigger (on trigger)");
    }
}
