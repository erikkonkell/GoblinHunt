using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

    private Transform playerTransform;
	// Use this for initialization
	void Start () {
        playerTransform = FindObjectOfType<vp_FPController>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = playerTransform.position;
        playerPos.y += 1f;
        transform.LookAt(playerPos);
	}
}
