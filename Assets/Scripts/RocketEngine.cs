using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

    public float fuelMass; // [kg]
    public float maxThrust;  // kN [kg m s^2]
    [Range(0f, 1f)]
    public float thrustPercent; //[none]
    public Vector3 thrustUnitVector; // [none]

    public string rocketName;
    public KeyCode input;

    private PhysicsEngine physics;

    private float currentThrust; // N 
    private float increaseThrust;

    public delegate void FuelUpdate(string name, float value);
    public static event FuelUpdate onFuelUpdate;

    public ParticleSystem engineParticle;
    private ParticleSystem.EmissionModule psEmit;

    private SoundManager m_source;


    // Use this for initialization
    void Start()
    {
        physics = GetComponent<PhysicsEngine>();
        physics.mass += fuelMass;
        increaseThrust = 1f / fuelMass;
        //Let the UI know the fuel
        if (onFuelUpdate != null)
        {
            onFuelUpdate(rocketName, fuelMass);
        }
        if(engineParticle)
        {
            psEmit = engineParticle.emission;
        }

        m_source = gameObject.GetComponent<SoundManager>();
    }

    public void UpdateFuelGUI()
    {
        if (onFuelUpdate != null)
        {
            onFuelUpdate(rocketName, fuelMass);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(input))
        {
            IncreaseThrust();
        }
        else
        {
            if(thrustPercent > 0f)
            {
                if (m_source)
                {
                    m_source.Stop();
                }
            }
            thrustPercent = 0f;
            if(engineParticle && engineParticle.isPlaying)
            {
                engineParticle.Stop();
                engineParticle.Clear();
            }

           
        }

        if(thrustPercent > 0.0f)
        {
            float nextFuelInUse = NextFuelUse();
            if (fuelMass > nextFuelInUse)
            {
                fuelMass -= nextFuelInUse;
                if(onFuelUpdate != null)
                {
                    onFuelUpdate(rocketName, fuelMass);
                }
                physics.mass -= nextFuelInUse;
                ExertForce();
                if (engineParticle && engineParticle.isStopped)
                {
                    psEmit.enabled = true;
                    engineParticle.Play();
                }
            }
            else
            {
                fuelMass = 0f;
                if (onFuelUpdate != null)
                {
                    onFuelUpdate(rocketName, fuelMass);
                }
            }
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

    void IncreaseThrust()
    {
        if(thrustPercent == 0)
        {
            if (m_source)
            {
                m_source.Play();
            }
        }

        thrustPercent = thrustPercent + increaseThrust;
    }
}
