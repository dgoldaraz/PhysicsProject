using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsEngine))]
public class AddForce : MonoBehaviour {

    public Vector3 force;
    private PhysicsEngine physics;


	// Use this for initialization
	void Start ()
    {
        physics = GetComponent<PhysicsEngine>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        physics.AddForce(force);
	}
}
