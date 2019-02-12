using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactGrenade : MonoBehaviour
{

    public float LifeTime = 3.0f;
    public float RigidbodyForce = 10.0f;        // this force will be applied to the rigidbody when spawned
    public float RigidbodySpin = 0.0f;          // this much random torque will be applied to rigidbody when spawned.
                                                // NOTE: spin is currently not recommended for use with the UFPS multiplayer add-on, since rigidbodies are not yet serialized!

    protected Rigidbody m_Rigidbody = null;
    protected Transform m_Source = null;                // immediate cause of the damage
    protected Transform m_OriginalSource = null;		// initial cause of the damage

    public Collider grenadeCol;
    private void Start()
    {
        //Physics.GetIgnoreLayerCollision(24, 30);
        //Physics.GetIgnoreLayerCollision(24, 27);
        Physics.IgnoreCollision(grenadeCol, FindObjectOfType<vp_FPController>().GetComponentInChildren<vp_DamageTransfer>().GetComponent<Collider>());
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
        {
            GetComponent<vp_DamageHandler>().Damage(1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            GetComponent<vp_DamageHandler>().Damage(1);
        }

    }
   

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
	}


	/// <summary>
	/// 
	/// </summary>
	protected virtual void OnEnable()
	{

		if (m_Rigidbody == null)
			return;

		// destroy the grenade object in 'lifetime' seconds. this will only work
		// if the object has a vp_DamageHandler-derived component on it
		

		// apply force on spawn
		if (RigidbodyForce != 0.0f)
			m_Rigidbody.AddForce((transform.forward * RigidbodyForce), ForceMode.Impulse); 
		if (RigidbodySpin != 0.0f)
			m_Rigidbody.AddTorque(Random.rotation.eulerAngles * RigidbodySpin);
		
	}


	/// <summary>
	/// sets the inflictor (original source) of any resulting damage.
	/// this is called by the 'vp_Shooter' script and is picked up by
	/// various other scripts, especially in UFPS multiplayer add-on.
	/// NOTE: this method must be called between spawning, and before
	/// 'OnEnable' is called.
	/// </summary>
	public void SetSource(Transform source)
	{
		m_Source = transform;
		m_OriginalSource = source;
	}

}
