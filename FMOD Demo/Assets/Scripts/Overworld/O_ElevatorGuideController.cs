using UnityEngine;
using System.Collections;

public class O_ElevatorGuideController : ActionObject
{
    public GuidePillar m_pillar;
    public O_Elevator m_elevator;
    public int m_floor;
    public float m_guideY;

    bool m_guideIsCurrent;

    void Start ()
    {
        m_guideIsCurrent = true;
	}
	void Update ()
    {
	
	}

    public override void ActionDown(GameObject sender)
    {
        if (m_guideIsCurrent)
        {
            m_guideIsCurrent = false;
            m_elevator.ChangeFloor(m_floor);
            m_pillar.Hide();
        }
        else
        {
            if (m_elevator.m_currentFloor != m_floor)
            {
                m_elevator.ChangeFloor(m_floor);
            }
            else
            {
                m_guideIsCurrent = true;
                m_pillar.Summon(m_guideY);
            }
        }
    }
}