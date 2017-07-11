using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float speed;

    private Rigidbody shot;


    // Use this for initialization
    void Start()
    {

        shot = GetComponent<Rigidbody>();
        shot.velocity = transform.forward * speed;


    }

}
