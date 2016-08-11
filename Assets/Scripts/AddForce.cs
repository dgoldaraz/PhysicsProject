using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsEngine))]
public class AddForce : MonoBehaviour {

    public Vector3 force;
    private PhysicsEngine physics;

    public bool countMass = true;
    public bool stopForce = false;

	// Use this for initialization
	void Start ()
    {
        physics = GetComponent<PhysicsEngine>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 finalForce = force;
        if(countMass)
        {
            finalForce *= physics.mass;
        }
        if(!stopForce)
        {
            physics.AddForce(force);
        }
	}
}
