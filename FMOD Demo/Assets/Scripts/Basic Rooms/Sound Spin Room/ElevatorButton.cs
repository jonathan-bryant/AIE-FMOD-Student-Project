/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Spin                                                      |
|   Fmod Related Scripting:     No                                                              |
|   Description:                This script simply changes the floor the elevator needs to go.  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class ElevatorButton : ActionObject
{
    public Elevator m_elevator;
    public int m_floor;
    public float m_height;
    FMODUnity.StudioEventEmitter m_buttonEvent;

    void Start()
    {
        m_buttonEvent = GetComponent<FMODUnity.StudioEventEmitter>();
        InitButton();
        InitGlow();
    }
    void Update()
    {
        UpdateGlow();
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_height);
        m_buttonEvent.Play();
    }
}
