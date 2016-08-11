using UnityEngine;
using System.Collections;

public class RocketPlataform : MonoBehaviour {

    public bool isFinalPlataform = true;

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
                if(isFinalPlataform)
                {
                    //Win!!!
                    Debug.Log("Win!!");
                    GameObject.FindObjectOfType<LevelManager>().LoadNextLevel();
                }
               
            }
            else
            {
                col.gameObject.GetComponent<Explosion>().Explode();
            }
        }
    }
}
