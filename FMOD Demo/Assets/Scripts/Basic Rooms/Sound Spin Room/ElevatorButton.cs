using UnityEngine;
using System.Collections;

public class ElevatorButton : ActionObject
{
    public FMODUnity.StudioEventEmitter m_event;
    bool m_isActive;
    public float m_duration;
    float m_elapsed;

    void Start()
    {
        m_isActive = false;
        m_elapsed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isActive)
        {
            m_elapsed += Time.deltaTime;
            if (m_elapsed >= m_duration)
            {
                m_isActive = false;
                m_event.SetParameter("Intensity", 1);
            }
            else
            {
                //Wiggle elevator
            }
        }
    }
    public override void ActionDown(GameObject sender)
    {
        if (!m_isActive)
        {
            m_event.SetParameter("Intensity", 0);
            m_event.Play();
            m_isActive = true;
            m_elapsed = 0.0f;
        }
    }
}
