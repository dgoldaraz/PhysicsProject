using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

    public float fuelMass; // [kg]
    public float maxThrust;  // kN [kg m s^2]
    [Range(0f, 1f)]
    public float thrustPercent; //[none]
    public Vector3 thrustUnitVector; // [none]

    private PhysicsEngine physics;

    private float currentThrust; // N 


    // Use this for initialization
    void Start()
    {
        physics = GetComponent<PhysicsEngine>();
        physics.mass += fuelMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float nextFuelInUse = NextFuelUse();
        if (fuelMass > nextFuelInUse)
        {
            fuelMass -= nextFuelInUse;
            physics.mass -= nextFuelInUse;
            ExertForce();
        }
        else
        {
            Debug.LogWarning("Out of rocket Fuel");
        }
        
    }

    float NextFuelUse()
    {
        float exhaustMassFlow; //[kg]
        float effectiveExhaustVelocity; //[m s^-1]

        //Fn = m * v => m = Fn / v
        effectiveExhaustVelocity = 4462f; // Bipropellant liquid rocket https://en.wikipedia.org/wiki/Specific_impulse#Specific_impulse_as_a_speed_.28effective_exhaust_velocity.29 
        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlow * Time.deltaTime; // [kg]
    }

    /// <summary>
    /// Decides the force to apply based on the thrust
    /// </summary>
    void ExertForce()
    {
        currentThrust = thrustPercent * maxThrust * 1000f;
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // [N]
        physics.AddForce(thrustVector);
    }
}
