/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			22/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public enum HELPERSTATE
{
    CLOSED,
    STOPPED,
    OPENING,
    LOADING,
};

public class HelperUIControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(0, 10.0f)]    public float m_timeUntilOpen = 2.0f;
    [Range(0, 10.0f)]    public float m_timeUntilClosed = 2.0f;
    [Range(0, 30.0f)]    public float m_maxPlayerDistance = 10.0f;
    public bool m_billboard = true;

    bool m_updateFacing = false;
    HELPERSTATE m_currentState = HELPERSTATE.CLOSED;

    GameObject m_playerRef = null;     // Used for getting the players position to billboard the UI.
    Animation m_uiAnimation;

	void Start () 
	{
        m_playerRef = GameObject.FindGameObjectWithTag("Player");
        m_uiAnimation = GetComponent<Animation>();
	}
	
	void Update () 
	{
        if (m_billboard && m_updateFacing)
        {
            transform.LookAt(Vector3.Lerp(transform.forward, m_playerRef.transform.position, 0.5f), Vector3.up);    // Lerp the facing direction to make smooth.
        }	
	}

    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, m_playerRef.transform.position) < m_maxPlayerDistance)
        {
            m_updateFacing = true;
        }
    }

    public void OpenHelper()
    {
        // Play opening animation
        m_currentState = HELPERSTATE.OPENING;
        OpenWaitAndLoad();
    }

    public void LoadHelper()
    {
        // Play animation to load
        m_currentState = HELPERSTATE.LOADING;
    }

    public void StopHelper()
    {
        if (m_uiAnimation.isPlaying)
        {
            m_uiAnimation.Stop();
            m_currentState = HELPERSTATE.STOPPED;
            WaitAndCloseHelper();
        }
    }

    public void CloseHelper()
    {
        // Play animation in reverse from current position
        m_uiAnimation[""].speed = -1;
        m_uiAnimation.Play("");
        m_currentState = HELPERSTATE.CLOSED;
    }

    IEnumerator OpenWaitAndLoad()
    {
        while (m_uiAnimation.isPlaying)
        {
            if (m_currentState != HELPERSTATE.OPENING)
            {
                break;
            }
            yield return null;
            LoadHelper();
        }
    }

    IEnumerator WaitAndCloseHelper()
    {
        while (m_currentState == HELPERSTATE.STOPPED)
        {
            yield return new WaitForSeconds(m_timeUntilClosed);
            CloseHelper();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (m_currentState)
        {
            case HELPERSTATE.CLOSED: OpenHelper();
                break;
            case HELPERSTATE.STOPPED: m_uiAnimation.Play();
                break;
            case HELPERSTATE.OPENING:
                break;
            case HELPERSTATE.LOADING:
                break;
            default:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopHelper();
    }
}
