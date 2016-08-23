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
    bool m_isAccelerating;

    void Start () {
        m_isAccelerating = false;
	}
	void Update () {
	    
	}

    public override void ActionPressed(GameObject sender)
    {
        m_car.Accelerate();
        m_isAccelerating = !m_isAccelerating;
        if (m_isAccelerating)
        {
            transform.Translate(new Vector3(0.0f, -0.25f, 0.0f));
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -25.0f);
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 25.0f);
            transform.Translate(new Vector3(0.0f, 0.25f, 0.0f));
        }
    }
}