using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PhysicsEngine : MonoBehaviour {

    public Vector3 velocityVector; //average velocity call in FixdUpdate  [m s^-1]

    public Vector3 netForce; //  N [kg m s^-2]
    public float mass = 1.0f; //[kg]
    private List<Vector3> forceVectorList = new List<Vector3>();

    public bool showTrails = true;
    private LineRenderer lineRenderer;

    // Use this for initialization
    void Start ()
    {
        InitDrawingTrails();
    }
	

    void InitDrawingTrails()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }

    /// <summary>
    /// This is call every 20 ms 
    /// </summary>
    void FixedUpdate()
    {
        DrawTrails(); 
        UpdatePosition();
    }

    /// <summary>
    /// Adds forces to the list
    /// </summary>
    /// <param name="newForce"></param>
    public void AddForce(Vector3 newForce)
    {
        forceVectorList.Add(newForce);
    }


    /// <summary>
    /// Updates the position dependant on the acceleration (forces) and velocity
    /// </summary>
    void UpdatePosition()
    {
        //First Newton law, if forces is 0, the change of velocity is 0
        //Second Netwon Law, F = ma => a = F/m
        //Third Law - There is an equal force in the inverse direction when an object hit another

        //SUm forces
        netForce = Vector3.zero;
        foreach (Vector3 f in forceVectorList)
        {
            netForce += f;
        }

        forceVectorList.Clear();

        //Change the velocity dependant of the forces;
        //Calculate acceleration 
        Vector3 acceleration = netForce / mass;
        velocityVector += acceleration * Time.deltaTime;

        //Update position
        transform.position += velocityVector * Time.deltaTime;
    }

    /// <summary>
    /// Draw the forces applied if it's neccesary
    /// </summary>
    void DrawTrails()
    {
        if (showTrails)
        {
            lineRenderer.enabled = true;
            int numberOfForces = forceVectorList.Count;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
