﻿using UnityEngine;
using System.Collections;

public class O_ElevatorGuideController : ActionObject
{
    public GuidePillar m_pillar;
    public O_Elevator m_elevator;
    public int m_floor;
    public float m_guideY;
    public float m_elevatorY;

    bool m_guideIsCurrent;

    void Start()
    {
        m_guideIsCurrent = true;
    }
    void Update()
    {

    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        if (m_guideIsCurrent)
        {
            m_guideIsCurrent = false;
            m_elevator.ChangeFloor(m_floor, m_elevatorY);
            m_pillar.Hide();
        }
        else
        {
            m_guideIsCurrent = true;
            m_pillar.Summon(m_guideY);
        }
    }
}