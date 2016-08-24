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
    STOPPED,
    OPENING,
    LOADING,
    IDLE
};

public class HelperUIControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(0, 10.0f)]
    public float m_timeUntilOpen = 2.0f;
    [Range(0, 10.0f)]
    public float m_timeUntilClosed = 2.0f;
    [Range(0, 30.0f)]
    public float m_maxPlayerDistance = 10.0f;
    public bool m_billboard = true;
    public string m_uiLoadAnim = "UI Prompt Load";
    public string m_uiOpenAnim = "UI Prompt Open";
    public string m_uiCloseAnim = "UI Prompt Close";

    bool m_updateFacing = false;
    HELPERSTATE m_currentState = HELPERSTATE.IDLE;

    GameObject m_playerRef = null;     // Used for getting the players position to billboard the UI.
    Animator m_uiAnimator;

    void Start()
    {
        m_playerRef = GameObject.FindGameObjectWithTag("Player");
        m_uiAnimator = GetComponentInChildren<Animator>();
        StopAnimation();
    }

    void Update()
    {
        if (m_billboard && m_updateFacing)
        {
            transform.LookAt(m_playerRef.transform.position, Vector3.up);    // Lerp the facing direction to make smooth.
            transform.Rotate(Vector3.up, 180.0f);
        }
    }

    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, m_playerRef.transform.position) < m_maxPlayerDistance)
        {
            m_updateFacing = true;
        }
    }

    public void LoadHelper()
    {
        // Play animation to load
        m_uiAnimator.SetTime(0);
        m_uiAnimator.Play(m_uiLoadAnim, 0, 0.0f);
        PlayForward();
        m_currentState = HELPERSTATE.LOADING;
        StartCoroutine(LoadWaitAndOpen());
    }

    IEnumerator LoadWaitAndOpen()
    {
        while (AnimationProgress() < 0.90f)
        {
            if (m_currentState != HELPERSTATE.LOADING)
            {
                break;
            }
            yield return null;
        }
        if (m_currentState == HELPERSTATE.LOADING)
            OpenHelper();
    }
    public void OpenHelper()
    {
        // Play opening animation
        m_uiAnimator.Play(m_uiOpenAnim, 0);
        m_currentState = HELPERSTATE.OPENING;
    }

    public void StopHelper()
    {
        // If the ui is not opening (allows the ui to open fully after loading)
        if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiLoadAnim) || 
            (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiOpenAnim) && m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1))
        {
            StopAnimation();
            m_currentState = HELPERSTATE.STOPPED;
            StartCoroutine(WaitAndCloseHelper());
        }
    }

    IEnumerator WaitAndCloseHelper()
    {
        while (m_currentState == HELPERSTATE.STOPPED)
        {
            yield return new WaitForSeconds(m_timeUntilClosed);

            // Play animation in reverse from current position
            if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiLoadAnim))
            {
                PlayBackwards();
                while (m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
                {
                    yield return null;
                }
            }
            else if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiOpenAnim) &&
                m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                m_uiAnimator.Play(m_uiCloseAnim, 0, 0f);
                PlayForward();
                while (m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    yield return null;
                }
            }

            m_currentState = HELPERSTATE.IDLE;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (m_currentState)
        {
            case HELPERSTATE.IDLE:
                //if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiCloseAnim))
                    LoadHelper();
                break;
            case HELPERSTATE.STOPPED:
                LoadHelper();

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

    void PlayForward()
    {
        m_uiAnimator.SetFloat("Speed", 1);
        m_uiAnimator.speed = 1;
    }

    void PlayBackwards()
    {
        m_uiAnimator.SetFloat("Speed", -1);
        m_uiAnimator.speed = 1;
    }

    void StopAnimation()
    {
        m_uiAnimator.speed = 0;
    }

    float AnimationProgress()
    {
        float totalTime = m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float fractionalTime = totalTime - (int)totalTime;

        if (totalTime >= 1 && fractionalTime == 0.0f)
        {
            return 1.0f;
        }

        return Mathf.Abs( fractionalTime );
    }
}
