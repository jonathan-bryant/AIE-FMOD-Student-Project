/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class InteractiveButton : ActionObject
{
    /*===============================================Fmod====================================================
    |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
    |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_emitter;
    public float m_value;
    ActorControls m_actor;

    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
    }
    void Update()
    {

    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        /*===============================================Fmod====================================================
        |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
        |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
        =======================================================================================================*/
        m_emitter.SetParameter("Intensity", m_value);
    }
}