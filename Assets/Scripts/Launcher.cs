using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{


    public float maxLaunchSpeed;
    public AudioClip launchShound;
    public AudioClip windUpSound;
    public GameObject ball;

    private float currentSpeed;
    private float speedIncrease;

    private AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = windUpSound;
        currentSpeed = 0.0f;
        speedIncrease = (maxLaunchSpeed * Time.fixedDeltaTime) / audioSource.clip.length;
    }

    void OnMouseDown()
    {
        //Increase ball speed to max for a few seconds
        //Consider InvokeRepeating
        currentSpeed = 0f;
        audioSource.PlayOneShot(windUpSound);
        InvokeRepeating("IncreaseSpeed", 0.5f, Time.fixedDeltaTime);
    }

    void OnMouseUp()
    {
        //Stop the ball and launch the ball
        audioSource.Stop();
        CancelInvoke("IncreaseSpeed");
        GameObject go = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;

        //Add this object to the UniversalGravitation 
        GameObject.FindObjectOfType<UniversalGravitation>().AddObject(go.GetComponent<PhysicsEngine>());
        Vector3 direction = new Vector3(-1.0f, 1.0f, 0.0f);//transform.localToWorldMatrix.MultiplyVector(transform.forward);
        Vector3 velocityBall = currentSpeed * direction.normalized;
        go.GetComponent<PhysicsEngine>().velocityVector = velocityBall;
        audioSource.PlayOneShot(launchShound);
    }

    void IncreaseSpeed()
    {
        if (currentSpeed < maxLaunchSpeed)
        {
            currentSpeed += speedIncrease;
        }
        else if (currentSpeed > maxLaunchSpeed)
        {
            currentSpeed = maxLaunchSpeed;
        }

    }
}
