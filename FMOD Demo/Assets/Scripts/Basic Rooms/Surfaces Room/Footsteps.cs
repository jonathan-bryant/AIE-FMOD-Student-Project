﻿/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Create oneshot sounds of footsteps and setting their parameters |
|   based on the type of floor currently set.                                                   |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
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
        m_currentParamValue = 0.0f;
    }
    void Update()
    {

    }

    public void PlayFootstep()
    {
        /*===============================================Fmod====================================================
        |   When the actors walking, create an instance of the footstep sound at every step. Creating a sound   |
        |   this way is known as a one shot sound, where we create an instance and release it, no longer        |
        |   in control of that instance.                                                                        |
        =======================================================================================================*/
        if (m_currentParamValue >= 1.0f)
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
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position - new Vector3(0, transform.localScale.y, 0)));
            /*===============================================Fmod====================================================
            |   The release function will remove control, which means calling functions such as setParamaterValue   |
            |   will do nothing.                                                                                    |
            =======================================================================================================*/
            instance.release();
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