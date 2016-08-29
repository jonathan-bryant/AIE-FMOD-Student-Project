/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
    ActorControls m_actor;
    public float m_walkSpeed, m_runSpeed;         /* Footsteps per second */
    float m_fpsElapsed;
    float m_currentParamValue;  /* carpet = 1.0f, grass = 2.0f, tile = 3.0f */

    /*===============================================Fmod====================================================
    |   This piece of code will allow the string m_footstepSurfaceName to use the event browser to select   |
    |   the event, in the inspector.                                                                        |
    =======================================================================================================*/
    [FMODUnity.EventRef]

    /*===============================================Fmod====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_footstepSurfaceName;
    
    void Start()
    {
        m_actor = GetComponent<ActorControls>();
        m_fpsElapsed = 0.0f;
        m_currentParamValue = 0.0f;
    }
    void Update()
    {
        m_fpsElapsed += Time.deltaTime;
        /*===============================================Fmod====================================================
        |   When the actors walking, create an instance of the footstep sound at every step. Creating a sound   |
        |   this way is known as a one shot sound, where we create an instance and release it, no longer        |
        |   in control of that instance.                                                                        |
        =======================================================================================================*/
        if (m_actor.CurrentVelocity.magnitude > 0.0f && m_actor.IsGrounded && m_fpsElapsed >= (m_actor.IsRunning ? m_runSpeed : m_walkSpeed))
        {
            if (m_currentParamValue > 0.99f)
            {
                /*===============================================Fmod====================================================
                |   The CreateInstance function takes in the name of the event as a parameter,                          |
                |   e.g. "event:/Basic Rooms/Footsteps".                                                                |
                |   This will simply create an instance.                                                                |
                =======================================================================================================*/
                FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
                /*===============================================Fmod====================================================
                |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
                |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
                =======================================================================================================*/
                instance.setParameterValue("Surface", m_currentParamValue);
                /*===============================================Fmod====================================================
                |   The start function will simply run the event.                                                       |
                =======================================================================================================*/
                instance.start();
                /*===============================================Fmod====================================================
                |   The set3DAttributes function is used to set the position of the audio. A Transform and rigidbody    |
                |   can be attached, instead of a Vector3 position, to the event, which will allow the sound to follow  |
                |   that transform.                                                                                     |
                =======================================================================================================*/
                instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position - new Vector3(0,transform.localScale.y,0)));
                /*===============================================Fmod====================================================
                |   The release function will remove control, which means calling functions such as setParamaterValue   |
                |   will do nothing.                                                                                    |
                =======================================================================================================*/
                instance.release();
            }
            m_fpsElapsed = 0.0f;
        }
    }
    void FixedUpdate()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit a_hit)
    {
        string name = a_hit.gameObject.name;
        string tag = a_hit.gameObject.tag;
        /*===============================================Fmod====================================================
        |   If the type of floor is either grass, wood or carpet, the next footstep will play the correct sound.|
        =======================================================================================================*/
        if (name.Contains("Grass") || tag == "Grass")
        {
            m_currentParamValue = 2.0f;
        }
        else if (name.Contains("Carpet") || tag == "Carpet")
        {
            m_currentParamValue = 1.0f;
        }
        else if (name.Contains("Tile") || tag == "Tile")
        {
            m_currentParamValue = 3.0f;
        }
        else
        {
            m_currentParamValue = 0.0f;
        }
    }
}