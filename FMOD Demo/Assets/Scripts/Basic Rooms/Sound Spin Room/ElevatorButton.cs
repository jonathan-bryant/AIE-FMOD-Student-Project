using UnityEngine;
using System.Collections;

public class ElevatorButton : ActionObject
{
    public Elevator m_elevator;
    public int m_floor;
    public float m_height;

    void Start()
    {
    }
    void Update()
    {
    }

    public override void ActionDown(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_height);
    }
}
