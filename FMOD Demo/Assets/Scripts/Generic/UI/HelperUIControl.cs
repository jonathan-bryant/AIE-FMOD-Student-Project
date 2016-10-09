/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			22/08/2016
==================================================================*/

using UnityEngine;
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
    [Range(0, 10.0f)]    public float m_timeUntilOpen = 2.0f;
    [Range(0, 10.0f)]    public float m_timeUntilClosed = 2.0f;
    [Range(0, 30.0f)]    public float m_maxPlayerDistance = 10.0f;
    public bool m_billboard = true;
    public string m_uiLoadAnim = "UI Prompt Load";
    public string m_uiOpenAnim = "UI Prompt Open";
    public string m_uiCloseAnim = "UI Prompt Close";

    /*===============================================Fmod====================================================
    |           Variables for creating and storing events to play during the UI animations.                 |
    =======================================================================================================*/
    [FMODUnity.EventRef]    public string m_uiHover;
    [FMODUnity.EventRef]    public string m_uiOpen;
    [FMODUnity.EventRef]    public string m_uiClose;
    FMOD.Studio.EventInstance m_currentEvent;
    
    bool m_updateFacing = false;
    HELPERSTATE m_currentState = HELPERSTATE.IDLE;
    float m_currentAnimationProgress;
    GameObject m_playerRef = null;     // Used for getting the players position to billboard the UI.
    Animator m_uiAnimator;

    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        m_playerRef = GameObject.FindGameObjectWithTag("Player");
        m_uiAnimator = GetComponentInChildren<Animator>();
        StopAnimation();

        /*===============================================Fmod====================================================
        |                           Create instanes of the sound events to use later.                           |
        =======================================================================================================*/
        //m_uiHoverEvent = FMODUnity.RuntimeManager.CreateInstance(m_uiHover);
        //m_uiOpenEvent = FMODUnity.RuntimeManager.CreateInstance(m_uiOpen);
        //m_uiCloseEvent = FMODUnity.RuntimeManager.CreateInstance(m_uiClose);
    }

    void FixedUpdate()
    {
        if (m_billboard && m_updateFacing)
        {
            transform.LookAt(m_playerRef.transform.position, Vector3.up);    // Lerp the facing direction to make smooth.
            transform.Rotate(Vector3.up, 180.0f);
        }

        m_currentAnimationProgress = AnimationProgress();
    }

    void LateUpdate()
    {
        if (m_playerRef != null)
        {
            if (Vector3.Distance(transform.position, m_playerRef.transform.position) < m_maxPlayerDistance)
            {
                m_updateFacing = true;
            }
        }
    }

    public void LoadHelper()
    {
        StopAllCoroutines();
        // Play loading animation for circle image
        m_uiAnimator.Play(m_uiLoadAnim, 0, 0.0f);
        // Set the animator time back to 0
        m_uiAnimator.SetTime(0);
        // Sets a modifying variable to positive for Forward.
        PlayForward();
        // Set the current state to loading as loading has started.
        m_currentState = HELPERSTATE.LOADING;
        // Run the coroutine that will then run the next animation as long as this animation has completed.
        StartCoroutine(LoadWaitAndOpen());
    }

    IEnumerator LoadWaitAndOpen()
    {
        m_currentEvent = FMODUnity.RuntimeManager.CreateInstance(m_uiHover);
        m_currentEvent.start();
        // Loop until the animation has played once.
        while (m_currentAnimationProgress < 0.95f)
        {
            // If at any point the cursor has left the image before it has completed the animation, 
            if (m_currentState != HELPERSTATE.LOADING)
            {
                break;
            }
            yield return null;
        }
        if (m_currentState == HELPERSTATE.LOADING)
        {
            m_currentEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            OpenHelper();
        }
    }
    public void OpenHelper()
    {
        m_currentEvent = FMODUnity.RuntimeManager.CreateInstance(m_uiOpen);
        m_currentEvent.start();

        // Play opening animation
        FMODUnity.RuntimeManager.PlayOneShot(m_uiOpen);
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
            m_currentEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
                while (m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 && m_uiAnimator.GetFloat("Speed") == -1)
                {
                    yield return null;
                }
            }
            else if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiOpenAnim) &&
                m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                m_uiAnimator.Play(m_uiCloseAnim, 0, 0f);
                PlayForward();
                FMODUnity.RuntimeManager.PlayOneShot(m_uiClose, GetComponent<RectTransform>().position);
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
