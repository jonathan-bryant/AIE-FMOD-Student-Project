/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Custom Distance Attenuation                                     |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Angles the cannon to the selected angle then fires at the given |
|   fire rate.                                                                                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour
{
    /*===============================================Fmod====================================================
    |   Create an event for the sound of the cannon changing height that starts up and loops until          |
    |   we set the parameter to tell the event to play the end sound and finish.                            |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    public string m_cannonSoundPath;
    FMOD.Studio.EventInstance m_cannonEvent;
    FMOD.Studio.ParameterInstance m_cannonStopParameter;
    FMOD.Studio.ParameterInstance m_cannonDirectionParameter;
    FMOD.Studio.PLAYBACK_STATE m_playState;

    [FMODUnity.EventRef]
    public string m_cannonFireSound;
    FMOD.Studio.EventInstance m_cannonFireEvent;

    public GameObject m_cannonBall;
    public GameObject m_cannon;
    float m_currentAngle;
    float m_power;
    float m_selectedAngle;
    bool m_isActive;
    
    public float m_fireDelay = 0.5f;
    float m_elapsedFireDelay;
    float m_cannonChangeSpeed = 10.0f;

    void Start()
    {
        /*===============================================Fmod====================================================
        |   Create the events, set the parameter for exiting the loop and set the 3D attributes.                |
        =======================================================================================================*/
        m_cannonEvent = FMODUnity.RuntimeManager.CreateInstance(m_cannonSoundPath);
        m_cannonEvent.getParameter("Stop Point", out m_cannonStopParameter);
        m_cannonEvent.getParameter("Direction", out m_cannonDirectionParameter);
        m_cannonEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        m_cannonFireEvent = FMODUnity.RuntimeManager.CreateInstance(m_cannonFireSound);
        m_cannonFireEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        
        m_currentAngle = 30.0f;
        m_selectedAngle = 30.0f;
        m_power = 10.0f;
        m_isActive = false;
    }
    void FixedUpdate()
    {
        if (m_isActive)
        {
            if (m_currentAngle != m_selectedAngle)
            {
                if (m_selectedAngle < m_currentAngle)
                {
                    /*===============================================Fmod====================================================
                    |   Check to see if the event is already playing, if not then start the event from the beginning.       |
                    =======================================================================================================*/
                    m_cannonEvent.getPlaybackState(out m_playState);
                    if (m_playState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        m_cannonStopParameter.setValue(0.0f);
                        m_cannonEvent.start();
                    }

                    m_currentAngle -= m_cannonChangeSpeed * Time.fixedDeltaTime;
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
                    /*===============================================Fmod====================================================
                    |   Check to see if the event is already playing, if not then start the event from the beginning.       |
                    =======================================================================================================*/
                    m_cannonEvent.getPlaybackState(out m_playState);
                    if (m_playState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        m_cannonStopParameter.setValue(0.0f);
                        m_cannonEvent.start();
                    }

                    m_currentAngle += m_cannonChangeSpeed * Time.fixedDeltaTime;
                    if (m_currentAngle > m_selectedAngle)
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
                m_cannonStopParameter.setValue(1.0f);
                m_elapsedFireDelay += Time.fixedDeltaTime;
                if (m_elapsedFireDelay >= m_fireDelay)
                {
                    m_elapsedFireDelay = 0.0f;
                    m_cannonFireEvent.start();
                    /*===============================================Fmod====================================================
                    |               Now we went to exit the event loop and play the end of the sound.                       |
                    =======================================================================================================*/
                    m_isActive = false;
                    GameObject ball = Instantiate(m_cannonBall, m_cannon.transform.GetChild(0).position - (m_cannon.transform.GetChild(0).up), Quaternion.identity) as GameObject;
                    ball.transform.SetParent(transform);
                    ball.GetComponent<Rigidbody>().AddForce(-m_cannon.transform.GetChild(0).up * m_power, ForceMode.Impulse);
                }
            }
        }
    }

    public void Fire(int a_index)
    {
        if (m_isActive)
            return;

        switch (a_index)
        {
            case 1:
                m_selectedAngle = 30.0f;
                m_power = 20.0f;
                m_isActive = true;
                break;
            case 2:
                m_selectedAngle = 45.0f;
                m_power = 15.0f;
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
        if (m_selectedAngle > m_currentAngle)
        {
            m_cannonStopParameter.setValue(1.0f);
        }
        else
        {
            m_cannonStopParameter.setValue(-1.0f);
        }
    }
}