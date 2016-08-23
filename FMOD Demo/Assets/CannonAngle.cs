using UnityEngine;
using System.Collections;

public class CannonAngle : ActionObject
{
    public bool m_up;
    public CannonController m_controller;

    void Start()
    {

    }
    void Update()
    {

    }

    public override void ActionPressed(GameObject sender)
    {
        if (m_up)
            m_controller.RaiseCannon();
        else
            m_controller.LowerCannon();
    }
    public override void ActionDown(GameObject sender)
    {
        if (m_up)
            m_controller.RaiseCannon();
        else
            m_controller.LowerCannon();
    }
}