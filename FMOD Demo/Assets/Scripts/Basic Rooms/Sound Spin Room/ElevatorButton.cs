using UnityEngine;
using System.Collections;

public class ElevatorButton : ActionObject
{
    public GameObject m_elevator;
    public GameObject m_doors;
    public FMODUnity.StudioEventEmitter m_event;
    bool m_isActive;
    public float m_duration;
    float m_elapsed;

    float m_originalZ;
    bool m_doorsOpen;

    void Start()
    {
        m_isActive = false;
        m_elapsed = 0.0f;
        m_originalZ = m_elevator.transform.position.z;
        m_doorsOpen = true;
    }
    void Update()
    {
        m_elapsed += Time.deltaTime;
        if(m_isActive)
        {
            if (m_doorsOpen)
            {
                if (m_elapsed >= 1.0f)
                {
                    m_doorsOpen = false;
                    m_event.SetParameter("Intensity", 0);
                    m_event.Play();
                }
            }
            else
            {
                if (m_elapsed >= m_duration)
                {
                    m_isActive = false;
                    m_elapsed = 0.0f;
                    m_event.SetParameter("Intensity", 1);
                }
                else
                {
                    Vector3 position = m_elevator.transform.position;
                    position.z = (Mathf.Sin(Time.time * 10.0f) * 0.05f) + m_originalZ;
                    m_elevator.transform.position = position;
                }
            }
        }
        else
        {
            if (m_elapsed >= 1.0f)
            {
                //m_doors.Open();
                m_doorsOpen = true;
            }
        }
    }
    public override void ActionDown(GameObject sender)
    {
        if (!m_isActive)
        {
            m_isActive = true;
            m_elapsed = 0.0f;
            //m_doors.Shut();
        }
    }
}
