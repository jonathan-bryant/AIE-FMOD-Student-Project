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

    public override void ActionDown(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_floorY);
    }
}
