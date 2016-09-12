using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

    public Vector3 windDirection;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rocket"))
        {
            AddForce addForceComponent = col.GetComponent<AddForce>();
            if(addForceComponent)
            {
                Vector3 cForce = addForceComponent.force;
                cForce += windDirection;
                addForceComponent.force = cForce;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Rocket"))
        {
            AddForce addForceComponent = col.GetComponent<AddForce>();
            if (addForceComponent)
            {
                Vector3 cForce = addForceComponent.force;
                cForce -= windDirection;
                addForceComponent.force = cForce;
            }
        }
    }
}
