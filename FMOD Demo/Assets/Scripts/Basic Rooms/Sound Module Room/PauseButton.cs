/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Module                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Calls Pause on the Orchestrion.                                 |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class PauseButton : ActionObject
{
    public Orchestrion m_orchestrion;
    FMODUnity.StudioEventEmitter m_buttonEvent;

    void Start()
    {
        InitButton();
        InitGlow();
        m_buttonEvent = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {
        UpdateGlow();
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_orchestrion.Pause();
        m_buttonEvent.Play();
    }
}