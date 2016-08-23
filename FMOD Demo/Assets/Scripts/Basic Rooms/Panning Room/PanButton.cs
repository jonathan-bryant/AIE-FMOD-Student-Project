/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class PanButton : ActionObject
{
    /*===============================================Fmod====================================================
    |   This StudioEventEmitter is a reference to an emitter created using Fmods scripts, in Unity.         |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_bgMusic;

    bool m_enablePan;
    public bool PanEnabled { get { return m_enablePan; } }
    float m_panElapsed;

    void Start()
    {
        m_panElapsed = 0.0f;
    }

    void Update()
    {
        if (m_enablePan)
        {
            m_panElapsed = Mathf.Min(1.0f, m_panElapsed + Time.deltaTime);
            /*===============================================Fmod====================================================
            |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
            |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
            =======================================================================================================*/
            m_bgMusic.SetParameter("Panning", m_panElapsed);
        }
        else
        {
            m_panElapsed = Mathf.Max(0.0f, m_panElapsed - Time.deltaTime);
            /*===============================================Fmod====================================================
            |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
            |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
            =======================================================================================================*/
            m_bgMusic.SetParameter("Panning", m_panElapsed);
        }
    }
    public override void ActionPressed(GameObject sender)
    {
        m_enablePan = !m_enablePan;
    }
}
