using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public GameObject explosionObject;

    void Start()
    {
        explosionObject.SetActive(false);
    }
    public void Explode()
    {
        //Explosion
        //Destroy Object
        explosionObject.SetActive(true);
        gameObject.GetComponent<PhysicsEngine>().FreezeMovement();
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in meshes)
        {
            m.enabled = false;
        }
        Destroy(gameObject,1.0f);
        GameObject.FindObjectOfType<GUIAndInputManager>().showButtons();
    }
}
