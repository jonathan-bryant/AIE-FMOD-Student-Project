﻿/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Plays Thunder.                                                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class LightningSound : MonoBehaviour
{
    /*===============================================FMOD====================================================
    |   Call this to display it in Unity Inspector.                                                         |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================FMOD====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_thunderPath;

    void Start()
    {
    }
    void Update()
    {
    }

    public void Play(int a_thunder)
    {
        /*===============================================FMOD====================================================
        |   EventInstance. Used to play or stop the sound, etc. In this case it will be used to create the event|
        |   and then immediately released. A one shot sound. Be sure not to loop a oneshot sound and release it.|
        |   Releasing a sound will destroy itself and free resources when stop is called or when it ends.       |
        =======================================================================================================*/
        FMOD.Studio.EventInstance m_thunderEvent;
        /*===============================================FMOD====================================================
        |   Calling this function will create an EventInstance. The return value is the created instance.       |
        =======================================================================================================*/
        m_thunderEvent = FMODUnity.RuntimeManager.CreateInstance(m_thunderPath);
        /*===============================================FMOD====================================================
        |   Calling this function will return a reference to a parameter inside EventInstance and store it in   |
        |   ParameterInstance.                                                                                  |
        =======================================================================================================*/
        m_thunderEvent.setParameterValue("Thunder", a_thunder);
        m_thunderEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, null));
        m_thunderEvent.start();
        m_thunderEvent.release();
    }
}