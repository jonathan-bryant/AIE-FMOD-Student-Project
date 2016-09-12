using UnityEngine;
using System.Collections;

public class SunController : ActionObject
{
    ActorControls m_actor;
    public Light m_sun;

    bool m_inControl;
    public float m_sunValue;
    public float SunValue { get { return m_sunValue; } }

    void Start()
    {
        m_sunValue = 0.0f;
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
    }

    void Update()
    {
        if(m_inControl)
        {
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0.0f)
            {
                transform.Rotate(new Vector3(0.0f, -mouseX, 0.0f));
                m_sun.transform.Rotate(new Vector3(1.0f,0.0f,0.0f), -mouseX);
                m_sunValue = m_sunValue + mouseX;
                while (m_sunValue >= 360.0f)
                {
                    m_sunValue -= 360.0f;
                }
                while(m_sunValue < 0.0f)
                {
                    m_sunValue += 360.0f;
                }
            }
        }
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_actor.m_disabledMouse = true;
        m_inControl = true;
    }
    public override void ActionReleased(GameObject sender, KeyCode a_key)
    {
        m_actor.m_disabledMouse = false;
        m_inControl = false;
    }
}
