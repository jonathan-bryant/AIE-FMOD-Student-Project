/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class Ignition : ActionObject
{
    public Car m_car;
    bool m_active;

	void Start () {
        m_active = false;
	}
	void Update () {
	    
	}

    protected override void Action(GameObject sender, bool a_use)
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
    }
}