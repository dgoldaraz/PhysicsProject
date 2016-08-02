using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PhysicsEngine : MonoBehaviour {

    public Vector3 velocityVector; //average velocity call in FixdUpdate

    public Vector3 netForces;
    public List<Vector3> forceVectorList = new List<Vector3>();
    public float mass = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// This is call every 20 ms 
    /// </summary>
    void FixedUpdate()
    {
        AddForces();
        //First Newtonlaw, move constantly if the net force is equal to 0
        //Second Netwon Law, F = ma => a = F/m
        UpdateVelocity();
        //Update position
        transform.position += velocityVector * Time.deltaTime;
        
    }

    void AddForces()
    {
        netForces = Vector3.zero;
        foreach(Vector3 f in forceVectorList)
        {
            netForces += f;
        }
    }

    void UpdateVelocity()
    {
        //Change the velocity dependant of the forces;
        //Calculate acceleration 
        Vector3 acceleration = netForces / mass;
        velocityVector += acceleration * Time.deltaTime;
    }
}
