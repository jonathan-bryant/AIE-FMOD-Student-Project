using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : ActionObject
{
    public string m_sceneName;
    public float m_angle;
    public float m_originalAngle;
    public float m_duration;
    float m_elapsed;
    float m_doorTransceiverElapsed;
    public float DoorElapsed
    {
        get
        {
            return m_doorTransceiverElapsed  / m_duration;
        }
    }
    bool m_doorOpen;
    public bool DoorOpen { get { return m_doorOpen; } }
    bool m_openingDoor, m_closingDoor;

    void Start()
    {
        m_openingDoor = false;
        m_closingDoor = false;
        m_elapsed = 0.0f;
        m_originalAngle = transform.eulerAngles.y;
    }

    void Update()
    {
        if (m_openingDoor)
        {
            m_elapsed += Time.deltaTime;
            m_doorTransceiverElapsed += Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_originalAngle + (m_angle * (m_elapsed / m_duration)), transform.eulerAngles.z);
            if (m_elapsed >= m_duration)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_originalAngle + m_angle, transform.eulerAngles.z);
                m_openingDoor = false;
                m_doorOpen = true;
                if (m_sceneName != null && m_sceneName != "")
                {
                    SceneManager.LoadScene(m_sceneName);
                }
                m_doorTransceiverElapsed = m_duration;
            }
        }
        else if (m_closingDoor)
        {
            m_elapsed += Time.deltaTime;
            m_doorTransceiverElapsed -= Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, (m_originalAngle + m_angle) - (m_angle * (m_elapsed / m_duration)), transform.eulerAngles.z);
            if (m_elapsed >= m_duration)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_originalAngle, transform.eulerAngles.z);
                m_closingDoor = false;
                m_doorOpen = false;
                m_doorTransceiverElapsed = 0.0f;
            }
        }
    }

    public override void Use(bool a_use)
    {
        if (!m_doorOpen)
        {
            m_elapsed = 0.0f;
            m_openingDoor = true;
            m_closingDoor = false;
        }
        else
        {
            m_elapsed = 0.0f;
            m_openingDoor = false;
            m_closingDoor = true;
        }
    }
}
