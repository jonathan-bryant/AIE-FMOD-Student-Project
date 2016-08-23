using UnityEngine;
using System.Collections;

public class CannonAngle : ActionObject
{
    public bool m_up;
    public CannonController m_controller;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Action(GameObject sender, bool a_use)
    {
        if (m_up)
            m_controller.RaiseCannon();
        else
            m_controller.LowerCannon();
    }
}
