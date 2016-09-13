/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class Pedal : ActionObject
{
    public Car m_car;
    float m_acceleration;

    void Start () {
        m_acceleration = 1.0f;
	}
	void Update () {
	    
	}

    public override void ActionDown(GameObject sender, KeyCode a_key)
    {
        if (a_key == m_actionKeys[0])
        {
            m_acceleration = Mathf.Min(m_acceleration + m_acceleration * Time.deltaTime * 5.0f, 5.0f);
        }
        else if(a_key == m_actionKeys[1])
        {
            m_acceleration = Mathf.Max(m_acceleration - m_acceleration * Time.deltaTime * 5.0f, 1.0f);
        }
        m_car.Accelerate(m_acceleration);
        Vector3 rot = transform.eulerAngles;
        rot.x = (m_acceleration / 5.0f) * 40.0f;
        transform.eulerAngles = rot;
    }
    public void Reset()
    {
        m_acceleration = 1.0f;
        Vector3 rot = transform.eulerAngles;
        rot.x = (m_acceleration / 5.0f) * 40.0f;
        transform.eulerAngles = rot;
    }
}