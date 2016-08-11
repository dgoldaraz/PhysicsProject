using UnityEngine;
using System.Collections;

public class RocketPlataform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rocket"))
        {
            //We are on the floor, stops the spaceShip if the velocity is less than the maxVelAllowed

            PhysicsEngine physiscsRigidBody = col.gameObject.GetComponent<PhysicsEngine>();
            if (physiscsRigidBody.velocityVector.magnitude < Manager.maxLandingSpeed)
            {
                physiscsRigidBody.FreezeMovement();
                AddForce[] forceComponent = col.gameObject.GetComponents<AddForce>();
                foreach (AddForce f in forceComponent)
                {
                    f.stopForce = true;
                }
                //Win!!!
                Debug.Log("Win!!");
                GameObject.FindObjectOfType<LevelManager>().LoadNextLevel();
            }
            else
            {
                col.gameObject.GetComponent<Explosion>().Explode();
            }
        }
    }
}
