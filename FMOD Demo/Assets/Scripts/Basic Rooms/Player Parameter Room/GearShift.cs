/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class GearShift : ActionObject
{
    public Car m_car;
    int m_gear;
    Vector3 m_startRotation;
    void Start ()
    {
        m_gear = 1;
        m_startRotation = transform.eulerAngles;
        Vector3 rot = transform.eulerAngles;
        rot.x = m_startRotation.x + (Mathf.Cos((m_gear * Mathf.PI) + Mathf.PI) * 4.0f);
        transform.eulerAngles = rot;
    }
	void Update ()
    {

	}

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        if(a_key == m_actionKeys[0])
        {
            m_car.UpGear();
            m_gear = Mathf.Min(++m_gear, 5);
        }
        else if(a_key == m_actionKeys[1])
        {
            m_car.DownGear();
            m_gear = Mathf.Max(--m_gear, 1);
        }
        Vector3 rot = transform.eulerAngles;
        rot.x = m_startRotation.x + (Mathf.Cos((m_gear * Mathf.PI) + Mathf.PI) * 4.0f);
        rot.z = m_startRotation.z - Mathf.FloorToInt((m_gear - 1) * 0.5f) * 4.0f;
        transform.eulerAngles = rot;
    }
    public void Reset()
    {
        m_gear = 1;
        Vector3 rot = transform.eulerAngles;
        rot.x = m_startRotation.x + (Mathf.Cos((m_gear * Mathf.PI) + Mathf.PI) * 4.0f);
        rot.z = m_startRotation.z - Mathf.FloorToInt((m_gear - 1) * 0.5f) * 4.0f;
        transform.eulerAngles = rot;
    }
}