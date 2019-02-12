using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponAttack : MonoBehaviour {

    public Vector3 Target;
    public float damage;
    public float speed;
    [Tooltip("Higher number gives more spray")]
    public float accuracy;
    //public bool collisionActive = false;

    private void Start()
    {
        Destroy(this.gameObject, Random.Range(5,10));
        Vector3 targetDir = Target - transform.position;
        float step = 720 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);

    }
    // Update is called once per frame
    void Update () {

        //transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        
        
	}
    public void Instantiate(Vector3 target)
    {
        Target = target;
        Target.y += Random.Range(-accuracy * 0.5f, accuracy * 0.5f);
        Target.x += Random.Range(-accuracy, accuracy);
        Target.z += Random.Range(-accuracy, accuracy);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("on Collision");
        if (collision.collider.tag == "Player" && collision.gameObject.GetComponent<vp_DamageTransfer>() != null)
        {
            collision.gameObject.GetComponent<vp_DamageTransfer>().Damage(damage);
        }
        //collisionActive = true;
        Destroy(this.gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on Trigger");
        if (other.tag == "Player" && other.gameObject.GetComponent<vp_DamageTransfer>() != null)
        {
            other.gameObject.GetComponent<vp_DamageTransfer>().Damage(damage);
        }
        //collisionActive = true;
        Destroy(this.gameObject);
    }
}
