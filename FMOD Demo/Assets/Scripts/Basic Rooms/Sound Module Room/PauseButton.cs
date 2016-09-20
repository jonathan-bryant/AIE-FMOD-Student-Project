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

public class PauseButton : MonoBehaviour
{
    public Orchestrion m_orchestrion;
    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter(Collider a_col)
    {
        m_orchestrion.Pause();
    }
}