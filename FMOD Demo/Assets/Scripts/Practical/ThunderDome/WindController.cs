/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls the wind, rotates the rain emitter aswell.             |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class WindController : ActionObject
{
    ActorControls m_actor;
    public ParticleSystem m_particleSystem;
    Vector3 m_minRainPosition;
    public Vector3 m_maxRainPosition;

    bool m_inControl;
    float m_windValue;
    public float WindValue { get { return m_windValue; } }

    float m_orignialX;

    void Start()
    {
        m_minRainPosition = m_particleSystem.transform.position;
        m_actor = Camera.main.GetComponentInParent<ActorControls>();

        m_windValue = 0.0f;
        var vol = m_particleSystem.forceOverLifetime;
        var x = vol.x;
        m_orignialX = x.constantMax;
        x.constantMax = 0.0f;
        vol.x = x;
    }

    void Update()
    {
        if (m_inControl)
        {
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0.0f)
            {
                if ((mouseX > 0.0f && m_windValue == 100.0f) || (mouseX < 0.0f && m_windValue == 0.0f))
                    return;

                m_windValue += mouseX;
                m_windValue = Mathf.Clamp(m_windValue, 0.0f, 100.0f);
                transform.Rotate(new Vector3(0.0f, -mouseX * 18.0f, 0.0f));

                var vol = m_particleSystem.forceOverLifetime;
                var x = vol.x;
                x.constantMax = Mathf.Lerp(0.0f, m_orignialX, m_windValue * 0.01f);
                vol.x = x;
                m_particleSystem.transform.position = Vector3.Lerp(m_minRainPosition, m_maxRainPosition, m_windValue * 0.01f);
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