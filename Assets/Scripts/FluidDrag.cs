using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsEngine))]

public class FluidDrag : MonoBehaviour {

    //Constants

    // In Low velocities we use v at the power of 1, but in High velocities we use v^2
    [Range(1, 2f)]
    public float velocityExponent; //[none]

    public float dragConstant; //[none]

    private PhysicsEngine physics;

	// Use this for initialization
	void Start ()
    {
        physics = GetComponent<PhysicsEngine>();
	}

    void FixedUpdate()
    {
        Vector3 velocityVector = physics.velocityVector;
        float speed = velocityVector.magnitude;
        float dragScale = CalculateDrag(speed);
        Vector3 dragVector = dragScale * -velocityVector.normalized;
        physics.AddForce( dragVector);
    }
    /// <summary>
    /// Calculates Drag based on the velocity passed
    /// </summary>
    /// <param name="velocity"></param>
    /// <returns></returns>
    float CalculateDrag(float speed)
    {
        return dragConstant * Mathf.Pow(speed, velocityExponent);
    }
}
