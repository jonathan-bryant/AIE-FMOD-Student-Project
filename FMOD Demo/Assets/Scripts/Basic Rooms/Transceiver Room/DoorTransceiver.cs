/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Transceiver                                                     |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                                                                                |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class DoorTransceiver : MonoBehaviour
{
    public Door m_door;

    /*===============================================Fmod====================================================
    |   This piece of code will allow the string m_footstepSurfaceName to use the event browser to select   |
    |   the event, in the inspector.                                                                        |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================Fmod====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_transceiverPath;
    /*===============================================Fmod====================================================
    |   EventInstance. Used to play or stop the sound, etc.                                                 |
    =======================================================================================================*/
    FMOD.Studio.EventInstance m_transceiverEvent;
    /*===============================================Fmod====================================================
    |   Parameter. Used to adjust EventInstances tracks. Such as: changing                                  |
    |   from wood to a carpet floor inside the same Event.                                                  |
    =======================================================================================================*/
    FMOD.Studio.ParameterInstance m_transceiverEnabledParameter;
    
    void Start()
    {
        /*===============================================Fmod====================================================
        |   The CreateInstance function takes in the name of the event as a parameter,                          |
        |   e.g. "event:/Basic Rooms/Footsteps".                                                                |
        |   This will simply create an instance.                                                                |
        =======================================================================================================*/
        m_transceiverEvent = FMODUnity.RuntimeManager.CreateInstance(m_transceiverPath);
        /*===============================================Fmod====================================================
        |   Get a reference to the surface paramater and store it in                                            |
        |   ParamaterInstance.                                                                                  |
        =======================================================================================================*/
        m_transceiverEvent.getParameter("Enabled", out m_transceiverEnabledParameter);
        /*===============================================Fmod====================================================
        |   The start function will simply run the event.                                                       |
        =======================================================================================================*/
        m_transceiverEvent.start();
        /*===============================================Fmod====================================================
        |   The AttachInstanceToGameObject function is used to set the position of the audio.                   |                                                           |
        =======================================================================================================*/
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_transceiverEvent, transform, null);
        /*===============================================Fmod====================================================
        |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
        |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
        =======================================================================================================*/
        m_transceiverEnabledParameter.setValue(0.0f);
    }    
    void Update()
    {
        /*===============================================Fmod====================================================
        |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
        |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
        =======================================================================================================*/
        m_transceiverEnabledParameter.setValue(m_door.DoorElapsed);
    }
}