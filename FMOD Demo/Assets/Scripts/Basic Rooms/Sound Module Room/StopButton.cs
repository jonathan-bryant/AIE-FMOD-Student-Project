/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Module                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Calls stop on the Orchestrion.                                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class StopButton : ActionObject
{
    public Orchestrion m_orchestrion;
    void Start()
    {

    }
    void Update()
    {

    }
    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_orchestrion.Stop();
    }
}