using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class AoeAttack : MonoBehaviour
{
    public float destoyDelay;
    private float damage;
    private Transform lookAt;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(lookAt);
        Destroy(this.gameObject, destoyDelay);
    }
    private void Update()
    {
        transform.LookAt(lookAt);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " :    OnTriggerEnter");
        if (other.gameObject.layer == vp_Layer.Trigger && other.gameObject.GetComponent<vp_DamageTransfer>() != null)
        {
            other.GetComponent<vp_DamageTransfer>().Damage(damage);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " :    OnCollisionEnter");
        if (collision.gameObject.layer == vp_Layer.Trigger && collision.gameObject.GetComponent<vp_DamageTransfer>() != null)
        {
            collision.gameObject.GetComponent<vp_DamageTransfer>().Damage(damage);
            Destroy(this.gameObject);
        }
    }
    public void spawnGamgeObject(float Damage, Transform LookAt)
    {
        damage = Damage;
        lookAt = LookAt;
    }
    
}
