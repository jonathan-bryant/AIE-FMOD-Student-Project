using UnityEngine;
using System.Collections;

public class ElevatorButton : ActionObject
{
    public Elevator m_elevator;
    public int m_floor;

    void Start()
    {
    }
    void Update()
    {
    }

    public override void ActionDown(GameObject sender)
    {
        m_elevator.ChangeFloor(m_floor);
    }
}
