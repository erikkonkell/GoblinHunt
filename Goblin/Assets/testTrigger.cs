using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == vp_Layer.Trigger && other.gameObject.GetComponentInParent<vp_DamageHandler>() != null)
            other.gameObject.GetComponentInParent<vp_DamageHandler>().Damage(1);
    }
}
