using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public GameObject explosionObject;
    public AudioClip explosionSound;


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
        AudioSource aSource = gameObject.GetComponent<AudioSource>();
        aSource.Stop();
        if(explosionSound)
        {
            aSource.clip = explosionSound;
            aSource.Play();
        }

        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in meshes)
        {
            m.enabled = false;
        }
        Destroy(gameObject,1.0f);
        GameObject.FindObjectOfType<GUIAndInputManager>().showButtons();
    }
}
