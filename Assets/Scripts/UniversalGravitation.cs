using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PhysicsEngine))]
public class UniversalGravitation : MonoBehaviour {


    private List<PhysicsEngine> physicsEngineArray;
    private const float G = 6.674e-11f; //[N m^2 kg ^-2]  [ m^3 s^-2 kg ^-1]

    // Use this for initialization
    void Start ()
    {
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>().OfType<PhysicsEngine>().ToList(); ;
    }

    void FixedUpdate()
    {
        CalculateGravitation();
    }

    public void AddObject(PhysicsEngine p)
    {
        physicsEngineArray.Add(p);
    }

    void CalculateGravitation()
    {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray)
        {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray)
            {
                if (physicsEngineA != physicsEngineB && physicsEngineA != this)
                {
                    //Calculate gravitational force F = G m1 x m2 / r ^ 2
                    //Debug.Log(" Calculating force on " + physicsEngineA.name + " by " + physicsEngineB.name);
                    Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
                    float distanceSquared = offset.sqrMagnitude;
                    float gravityForce = G * ((physicsEngineB.mass * physicsEngineA.mass) / distanceSquared);
                    Vector3 gravityFeltVector = gravityForce * offset.normalized;
                    physicsEngineA.AddForce(-gravityFeltVector);
                }
            }

        }
    }
}
