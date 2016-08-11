﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIAndInputManager : MonoBehaviour {


    private Button[] buttons;
    public Text LeftFuel;
    public Text RightFuel;
    public Text CenterFuel;


    // Use this for initialization
    void Start ()
    {
        buttons = GameObject.FindObjectsOfType<Button>();
        hideButtons();

        RocketEngine.onFuelUpdate += updateText;
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
}
