using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gibs : MonoBehaviour
{

    public AudioSource gibSource;
    public int loops = 1;

    private int g = 0;
    // Start is called before the first frame update
    void Start()
    {
        gibSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (g < loops)
        {
            gibSource.Play();
            g++;
        }


    }
}
