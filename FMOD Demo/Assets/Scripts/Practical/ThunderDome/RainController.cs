/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls the rain value and the particle emitter.               |
|   WeatherController script will access this script.                                           |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class RainController : ActionObject
{
    ActorControls m_actor;
    public ParticleSystem m_particleSystem;

    bool m_inControl;
    float m_rainValue;
    public float RainValue { get { return m_rainValue; } }

    float m_originalRate;
    float m_originalSpeed;

    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();

        m_rainValue = 0.0f;
        m_originalRate = m_particleSystem.emission.rate.constantMax;
        m_originalSpeed = m_particleSystem.startSpeed;

        var emission = m_particleSystem.emission;

        var rate = emission.rate;
        rate.constantMax = Mathf.Lerp(0, m_originalRate, m_rainValue);
        emission.rate = rate;

        m_particleSystem.startSpeed = Mathf.Lerp(0.0f, m_originalSpeed, m_rainValue);
    }

    void Update()
    {
        if (m_inControl)
        {
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0.0f)
            {
                if ((mouseX > 0.0f && m_rainValue == 1.0f) || (mouseX < 0.0f && m_rainValue == 0.0f))
                    return;

                m_rainValue += mouseX / 100.0f;
                m_rainValue = Mathf.Clamp(m_rainValue, 0.0f, 1.0f);
                transform.Rotate(new Vector3(0.0f, -mouseX * 18.0f, 0.0f));

                var emission = m_particleSystem.emission;

                var rate = emission.rate;
                rate.constantMax = Mathf.Lerp(0, m_originalRate, m_rainValue);
                emission.rate = rate;

                m_particleSystem.startSpeed = Mathf.Lerp(0.0f, m_originalSpeed, m_rainValue);
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