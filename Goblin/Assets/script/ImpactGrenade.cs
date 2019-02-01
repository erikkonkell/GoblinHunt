using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactGrenade : MonoBehaviour
{
    private void Start()
    {
        CapsuleCollider grenadeCol;
    }
    private void OnCollisionEnter(Collision col)
    {
        GetComponent<vp_DamageHandler>().Damage(1);
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<vp_DamageHandler>().Damage(1);
    }

}
