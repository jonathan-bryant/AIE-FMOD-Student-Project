/*===============================================================================================
|  Project:		FMOD Demo                                                                       |
|  Developer:	Matthew Zelenko                                                                 |
|  Company:		FMOD                                                                            |
|  Date:		20/09/2016                                                                      |
================================================================================================*/
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
    }
}