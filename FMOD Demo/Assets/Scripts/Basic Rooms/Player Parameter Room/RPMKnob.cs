/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class RPMKnob : ActionObject
{
    ActorControls m_actor;
    float m_rpmValue;
    bool m_inControl;
    /*===============================================Fmod====================================================
    |   This StudioEventEmitter is a reference to an emitter created using Fmods scripts, in Unity.         |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_emitter;
    
    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
        m_rpmValue = 0.0f;
        m_inControl = false;
    }
    void Update()
    {
        if (m_inControl)
        {
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0.0f)
            {
                if ((mouseX > 0.0f && m_rpmValue == 2000.0f) || (mouseX < 0.0f && m_rpmValue == 0.0f))
                    return;
                m_rpmValue += mouseX * 10.0f;
                m_rpmValue = Mathf.Clamp(m_rpmValue, 0.0f, 2000.0f);
                transform.Rotate(new Vector3(0.0f, -mouseX * 10.0f, 0.0f));
                m_emitter.SetParameter("RPM", m_rpmValue);
            }
        }
    }

    override protected void Action(GameObject sender, bool a_use)
    {
        if (a_use)
        {
            m_actor.m_disabledMouse = true;
            m_inControl = true;
        }
        else
        {
            m_actor.m_disabledMouse = false;
            m_inControl = false;
        }
    }
}