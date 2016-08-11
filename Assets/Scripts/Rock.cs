using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rocket"))
        {
            col.gameObject.GetComponent<Explosion>().Explode();
        }
    }
}
