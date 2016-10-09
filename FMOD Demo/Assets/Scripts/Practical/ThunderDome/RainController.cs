
using UnityEngine;
using System.Collections;

public class RainController : ActionObject
{
    public Transform m_pond;
    float m_minPondHeight;
    public float m_maxPondHeight;
    ActorControls m_actor;
    public ParticleSystem m_particleSystem;

    bool m_inControl;
    float m_rainValue;
    float m_waterValue;
    public float RainValue { get { return m_rainValue; } }
    public float WaterValue { get { return m_waterValue; } }

    float m_originalRate;

    void Start()
    {
        m_minPondHeight = m_pond.position.y;
        m_actor = Camera.main.GetComponentInParent<ActorControls>();

        m_rainValue = 0.0f;
        m_originalRate = m_particleSystem.emission.rate.constantMax;

        var emission = m_particleSystem.emission;

        var rate = emission.rate;
        rate.constantMax = Mathf.Lerp(0, m_originalRate, m_rainValue);
        emission.rate = rate;
    }

    void Update()
    {
        if (m_inControl)
        {
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0.0f)
            {
                if (!((mouseX > 0.0f && m_rainValue == 4.0f) || (mouseX < 0.0f && m_rainValue == 0.0f)))
                {
                    m_rainValue += mouseX;
                    m_rainValue = Mathf.Clamp(m_rainValue, 0.0f, 4.0f);
                    transform.Rotate(new Vector3(0.0f, -mouseX * 18.0f, 0.0f));

                    var emission = m_particleSystem.emission;

                    var rate = emission.rate;
                    rate.constantMax = Mathf.Lerp(0, m_originalRate, m_rainValue);
                    emission.rate = rate;
                }
            }
        }
        if(m_waterValue != m_rainValue)
        {
            m_waterValue = Mathf.MoveTowards(m_waterValue, m_rainValue, Time.deltaTime * 0.25f);
            Vector3 pos = m_pond.position;
            pos.y = Mathf.Lerp(m_minPondHeight, m_maxPondHeight, Mathf.Clamp(m_waterValue - 2.0f, 0.0f, 2.0f) * 0.5f);
            m_pond.position = pos;
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