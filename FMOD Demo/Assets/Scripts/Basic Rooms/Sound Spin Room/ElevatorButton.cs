/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Spin                                                      |
|   Fmod Related Scripting:     Yes                                                              |
|   Description:                This script simply changes the floor the elevator needs to go.  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class ElevatorButton : ActionObject
{
    public Elevator m_elevator;
    public int m_floor;
    public Vector3 m_position;
    [FMODUnity.EventRef]
    public string m_event;

    void Start()
    {
        InitButton();
        InitGlow();
    }
    void Update()
    {
        UpdateGlow();
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_position);
        /*===============================================FMOD====================================================
        |   You can play a oneshot sound using the RuntimeManager, it just needs the event name and a position. |
        =======================================================================================================*/
        FMODUnity.RuntimeManager.PlayOneShot(m_event, transform.position);
    }
}
