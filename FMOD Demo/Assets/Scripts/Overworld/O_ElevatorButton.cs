/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Tells the elevator to change floors.                            |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class O_ElevatorButton : ActionObject
{
    public O_Elevator m_elevator;
    public int m_floor;
    public float m_floorY;

    void Start()
    {
    }
    void Update()
    {
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_floorY);
    }
    public override void ActionDown(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_floorY);
    }
}
