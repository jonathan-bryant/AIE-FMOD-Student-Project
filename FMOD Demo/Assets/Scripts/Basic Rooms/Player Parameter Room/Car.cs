/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
    public GameObject m_ignition;
    public GameObject m_pedal;
    public GameObject m_upGear, m_downGear;

    /*===============================================Fmod====================================================
    |   This is where the StudioEventEmitter component will be stored                                      |
    =======================================================================================================*/
    FMODUnity.StudioEventEmitter m_sound;

    bool m_isActive;
    bool m_isAccelerating;
    float m_rpm;
    float m_load;
    int m_gear;

    void Start()
    {
        m_gear = 1;
        m_isActive = false;
        m_isAccelerating = false;
        m_rpm = 0.0f;
        m_load = -1.0f;
        /*===============================================Fmod====================================================
        |   This is simply getting the attached StudioEventEmitter component.                                   |
        =======================================================================================================*/
        m_sound = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {
        if (m_isActive)
        {
            if (m_isAccelerating || m_rpm < 400.0f)
            {
                m_rpm += Time.deltaTime * 400.0f * 1 / m_gear;
                m_rpm = Mathf.Min(2000.0f, m_rpm);
            }
            else
            {
                m_rpm -= Time.deltaTime * 600.0f * 1 / m_gear;
                m_rpm = Mathf.Max(400.0f, m_rpm);
            }
        }
        else
        {
            m_rpm -= Time.deltaTime * 1600.0f * 1 / m_gear;
            m_rpm = Mathf.Max(0.0f, m_rpm);
        }
        /*===============================================Fmod====================================================
        |   The SetParameter function simply sets the event parameter to the value passed in.                   |
        |   The first param is the name of the parameter, the second is the value.                              |
        |   To see what parameters looks like in studio, open the file:                                         |
        |   FMOD\Fmod Demo Sounds\Fmod Demo Sounds.fspro                                                        |
        =======================================================================================================*/
        m_sound.SetParameter("RPM", m_rpm);
    }

    public void IgnitionOn()
    {
        m_isActive = true;
    }
    public void IgnitionOff()
    {
        m_isActive = false;
    }
    public void UpGear()
    {
        if (m_gear < 5)
        {
            m_gear++;
            m_rpm -= 500;
            m_rpm = Mathf.Max(m_rpm, 1000.0f);
        }
    }
    public void DownGear()
    {
        if (m_gear > 1)
        {
            m_gear--;
            m_rpm += 500;
            m_rpm = Mathf.Min(2000.0f, m_rpm);
        }
    }
    public void Accelerate()
    {
        m_isAccelerating = !m_isAccelerating;
    }
    public void UpLoad()
    {
        m_load += 0.1f;
        m_load = Mathf.Min(1.0f, m_load);
        /*===============================================Fmod====================================================
        |   The SetParameter function simply sets the event parameter to the value passed in.                   |
        |   The first param is the name of the parameter, the second is the value.                              |
        |   To see what parameters looks like in studio, open the file:                                         |
        |   FMOD\Fmod Demo Sounds\Fmod Demo Sounds.fspro                                                        |
        =======================================================================================================*/
        m_sound.SetParameter("Load", m_load);
    }
    public void DownLoad()
    {
        m_load -= 0.1f;
        m_load = Mathf.Max(-1.0f, m_load);
        /*===============================================Fmod====================================================
        |   The SetParameter function simply sets the event parameter to the value passed in.                   |
        |   The first param is the name of the parameter, the second is the value.                              |
        |   To see what parameters looks like in studio, open the file:                                         |
        |   FMOD\Fmod Demo Sounds\Fmod Demo Sounds.fspro                                                        |
        =======================================================================================================*/
        m_sound.SetParameter("Load", m_load);
    }
}