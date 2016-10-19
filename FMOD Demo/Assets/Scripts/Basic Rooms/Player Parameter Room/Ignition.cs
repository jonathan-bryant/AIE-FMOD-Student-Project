﻿/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Player Parameter                                                |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Tells the car to turn the ignition on or off.                   |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Ignition : ActionObject
{
    public Car m_car;
    bool m_active;
    FMODUnity.StudioEventEmitter m_buttonEvent;

	void Start ()
    {
        InitGlow();
        StopGlow();
        m_active = false;
        m_buttonEvent = GetComponent<FMODUnity.StudioEventEmitter>();
    }
	void Update ()
    {
        InitButton();
        UpdateGlow();
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_active = !m_active;
        if(m_active == true)
        {
            m_car.IgnitionOn(); 
        }
        else
        {
            m_car.IgnitionOff();
        }
        m_buttonEvent.Play();
    }
}