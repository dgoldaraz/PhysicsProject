using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rocket"))
        {
            //We are on the floor, stops the spaceShip if the velocity is less than the maxVelAllowed

            PhysicsEngine physiscsRigidBody = col.gameObject.GetComponent<PhysicsEngine>();
            if (physiscsRigidBody.velocityVector.magnitude * 100f < Manager.maxLandingSpeed)
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
