/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Controls the elevator.                                          |
===============================================================================================*/
using UnityEngine;
using System.Collections;

class NewElevator : MonoBehaviour
{
    /*===============================================Fmod====================================================
    |   This line is a way to get an existing eventEmitter and control it from script                       |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_event;
    public FMODUnity.StudioEventEmitter m_elevatorMusic;

    ActorControls m_actor;

    void start()
    {

    }
    void Update()
    {

    }
}