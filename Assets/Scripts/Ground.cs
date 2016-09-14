using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{
    public float maxLandingSpeed = 100f;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rocket"))
        {
            //We are on the floor, stops the spaceShip if the velocity is less than the maxVelAllowed

            PhysicsEngine physiscsRigidBody = col.gameObject.GetComponent<PhysicsEngine>();
            if (physiscsRigidBody.velocityVector.magnitude * 100f < maxLandingSpeed)
            {
                physiscsRigidBody.FreezeMovement();
                AddForce[] forceComponent = col.gameObject.GetComponents<AddForce>();
                foreach (AddForce f in forceComponent)
                {
                    f.stopForce = true;
                }
            }
            else
            {
                col.gameObject.GetComponent<Explosion>().Explode();
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Rocket"))
        {
            //Reactivate forces
            AddForce[] forceComponent = col.gameObject.GetComponents<AddForce>();
            foreach (AddForce f in forceComponent)
            {
                f.stopForce = false;
            }
        }

     }
}
