/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Custom Distance Attenuation                                     |
|   Fmod Related Scripting:     Yes                                                              |
|   Description:                Angles the cannon to the selected angle then fires at the given |
|   fire rate.                                                                                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour
{
    [FMODUnity.EventRef]    public string m_cannonUpSound;
    FMOD.Studio.EventInstance m_cannonUpEvent;
    FMOD.Studio.ParameterInstance m_cannonUpStop;
    [FMODUnity.EventRef]    public string m_cannonDownSound;
    FMOD.Studio.EventInstance m_cannonDownEvent;
    FMOD.Studio.ParameterInstance m_cannonDownStop;
    FMOD.Studio.PLAYBACK_STATE m_playState;

    public GameObject m_cannonBall;
    public GameObject m_cannon;
    float m_currentAngle;
    float m_power;
    float m_selectedAngle;
    bool m_isActive;

    public float m_timer;
    float m_elapsed;

    void Start()
    {
        m_cannonUpEvent = FMODUnity.RuntimeManager.CreateInstance(m_cannonUpSound);
        m_cannonUpEvent.getParameter("Stop Point", out m_cannonUpStop);
        m_cannonUpEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        m_cannonDownEvent = FMODUnity.RuntimeManager.CreateInstance(m_cannonDownSound);
        m_cannonDownEvent.getParameter("Stop Point", out m_cannonDownStop);
        m_cannonDownEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        m_elapsed = 0.0f;
        m_currentAngle = 30.0f;
        m_selectedAngle = 30.0f;
        m_power = 10.0f;
        m_isActive = false;
    }
    void FixedUpdate()
    {
        m_elapsed += Time.fixedDeltaTime;
        if (m_isActive)
        {
            if (m_currentAngle != m_selectedAngle)
            {
                if (m_selectedAngle < m_currentAngle)
                {
                    m_cannonUpEvent.getPlaybackState(out m_playState);
                    if (m_playState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        m_cannonUpStop.setValue(0.0f);
                        m_cannonUpEvent.start();
                    }

                    m_currentAngle -=  5.0f * Time.fixedDeltaTime;
                    if (m_currentAngle < m_selectedAngle)
                    {
                        m_currentAngle = m_selectedAngle;
                    }
                    Vector3 rot = m_cannon.transform.eulerAngles;
                    rot.x = m_currentAngle;
                    m_cannon.transform.eulerAngles = rot;
                }
                else
                {
                    m_cannonDownEvent.getPlaybackState(out m_playState);
                    if (m_playState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        m_cannonDownStop.setValue(0.0f);
                        m_cannonDownEvent.start();
                    }

                    m_currentAngle += 5.0f * Time.fixedDeltaTime;
                    if(m_currentAngle > m_selectedAngle)
                    {
                        m_currentAngle = m_selectedAngle;
                    }
                    Vector3 rot = m_cannon.transform.eulerAngles;
                    rot.x = m_currentAngle;
                    m_cannon.transform.eulerAngles = rot;
                }
            }
            else
            {
                m_cannonDownStop.setValue(1.0f);
                m_cannonUpStop.setValue(1.0f);
                m_elapsed = 0.0f;
                m_isActive = false;
                GameObject ball = Instantiate(m_cannonBall, m_cannon.transform.GetChild(0).position - (m_cannon.transform.GetChild(0).up), Quaternion.identity) as GameObject;
                ball.transform.SetParent(transform);
                ball.GetComponent<Rigidbody>().AddForce(-m_cannon.transform.GetChild(0).up * m_power, ForceMode.Impulse);
            }
        }
    }

    public void Fire(int a_index)
    {
        if (m_isActive)
            return;
        if (m_elapsed < m_timer)
            return;
        m_elapsed = 0.0f;

        switch (a_index)
        {
            case 1:
                m_selectedAngle = 30.0f;
                m_power = 15.0f;
                m_isActive = true;
                break;
            case 2:
                m_selectedAngle = 45.0f;
                m_power = 20.0f;
                m_isActive = true;
                break;
            case 3:
                m_selectedAngle = 60.0f;
                m_power = 10.0f;
                m_isActive = true;
                break;
            default:
                break;
        }
    }
}
