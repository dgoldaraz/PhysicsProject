using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIAndInputManager : MonoBehaviour {


    private Button[] buttons;
    public Text LeftFuel;
    public Text RightFuel;
    public Text CenterFuel;
    public Text speedText;


    // Use this for initialization
    void Start ()
    {
        buttons = GameObject.FindObjectsOfType<Button>();
        hideButtons();

        RocketEngine.onFuelUpdate += updateText;
        InvokeRepeating("UpdateSpeed", 0.0f, 1.0f);
	}

    void OnDestroy()
    {
        RocketEngine.onFuelUpdate -= updateText;
    }


    // Update is called once per frame
    void Update ()
    {
	}

    void hideButtons()
    {
        foreach( Button b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void showButtons()
    {
        foreach (Button b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    void updateText(string name, float value)
    {
        if(name == "Left")
        {
            LeftFuel.text = "Left Fuel: " + value.ToString();
            if(value == 0f)
            {
                //No Fuel!!
                LeftFuel.color = Color.red;
            }
        }
        if (name == "Right")
        {
            RightFuel.text = "Right Fuel: " + value.ToString();
            if (value == 0f)
            {
                //No Fuel!!
                RightFuel.color = Color.red;
            }
        }
        if (name == "Up")
        {
            CenterFuel.text = "Center Fuel: " + value.ToString();
            if (value == 0f)
            {
                //No Fuel!!
                CenterFuel.color = Color.red;
            }
        }
    }

    void UpdateSpeed()
    {
        GameObject player= GameObject.FindGameObjectWithTag("Rocket");
        if(player)
        {
            int speed =(int)(player.GetComponent<PhysicsEngine>().velocityVector.magnitude * 100f);
            speedText.text = "Speed: " + speed.ToString() + "km/s";
            Debug.Log(speed);
        }
    }
}
