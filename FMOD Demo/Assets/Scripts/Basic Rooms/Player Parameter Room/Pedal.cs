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

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_car.Accelerate();
        m_isAccelerating = !m_isAccelerating;
    }
}