/*=================================================================
Project:		FMOD Unity Plugin Demo
Developer:		Cameron Baron
Company:		Firelight Technologies
Date:			26/09/2016
Scene:          Overworld
Fmod Related:   Yes
Description:    Controls the doors opening and closing as well as 
                loading and unload scenes and FMOD Studio banks.
==================================================================*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class TwoDoorController : ActionObject
{
    // Public Vars
    public GameObject m_upperDoor;      // Ref to upper door object that gets moved.
    public GameObject m_lowerDoor;      // Ref to lower door object that gets moved.

    public string m_sceneToLoad;        // Name of scene to be loaded/unloaded (needs to be exact).
    public float m_doorHoldTime;        // Amount of time the door will stay open after leaving the trigger area before closing.
    public float m_unloadDelay;         // Time until scene is unloaded once door has fully closed.
    public RoomCompleted m_completeSign;

    //---------------------------------Fmod-------------------------------
    //  Bank reference is the same as and Event, except using BankRef.
    //  Another difference between Events and Banks is loading and 
    //  unloading is done using the string and not a class or instance.
    //--------------------------------------------------------------------
    [FMODUnity.BankRef]
    public string m_bankToLoad;

    [FMODUnity.EventRef]
    public string m_doorSound;
    [Range(0, 4)]    public float m_doorReverb = 2.0f;

    // Private Vars
    FMOD.Studio.EventInstance m_doorEvent;
    FMOD.Studio.ParameterInstance m_reverbAmount;
    FMOD.Studio.ParameterInstance m_direction;

    float m_upperDistToNewPos;				        // Distance between upper door and the current target position.
    float m_lowerDistToNewPos;				        // Distance between lower door and the current target position.
    bool m_opening = false;				            // Bool used to determine which way the door will move (open/close).
    Vector3 m_upperOpenPos, m_upperClosedPos;       // Positions of upper door (in local space) when open/closed.
    Vector3 m_lowerOpenPos, m_lowerClosedPos;       // Positions of lower door (in local space) when open/closed.

    bool m_completed = false;

    static AsyncOperation s_async;

    void Start()
    {
        if (m_sceneToLoad == "")
        {
            Renderer rend = m_lowerDoor.GetComponent<Renderer>();
            rend.materials[2].SetColor("_EmissionColor", Color.red);
            DynamicGI.SetEmissive(rend, Color.red);
			Destroy (this);
        }

        if (m_doorSound != "")
        {
            m_doorEvent = FMODUnity.RuntimeManager.CreateInstance(m_doorSound);
            m_doorEvent.getParameter("Reverb", out m_reverbAmount);
            m_doorEvent.getParameter("Direction", out m_direction);
        }
        
        Application.backgroundLoadingPriority = ThreadPriority.Low;		// Setting the thread priority to low forces the async operations to use less cpu.

        m_upperClosedPos = m_upperDoor.transform.localPosition;
        m_upperOpenPos = m_upperClosedPos + (Vector3.up * (m_upperDoor.transform.localScale.y + 0.2f));

        m_lowerClosedPos = m_lowerDoor.transform.localPosition;
        m_lowerOpenPos = m_lowerClosedPos - (Vector3.up * (m_lowerDoor.transform.localScale.y + 0.2f));
    }

    void FixedUpdate()
    {
        m_upperDistToNewPos = Vector3.Distance(m_upperDoor.transform.localPosition, (m_opening ? m_upperOpenPos : m_upperClosedPos));
        if (m_upperDistToNewPos > 0.1f)
        {
            m_upperDoor.transform.localPosition += Vector3.up * Time.deltaTime * (m_opening ? 1 : -1);
        }
        else
        {
            m_upperDoor.transform.localPosition = (m_opening ? m_upperOpenPos : m_upperClosedPos);
        }

        m_lowerDistToNewPos = Vector3.Distance(m_lowerDoor.transform.localPosition, (m_opening ? m_lowerOpenPos : m_lowerClosedPos));

        if (m_lowerDistToNewPos > 0.1f)
        {
            m_lowerDoor.transform.localPosition -= Vector3.up * Time.deltaTime * (m_opening ? 1 : -1);
        }
        else
        {
            m_lowerDoor.transform.localPosition = (m_opening ? m_lowerOpenPos : m_lowerClosedPos);
        }

        if (m_completed)
        {
            Renderer rend = m_lowerDoor.GetComponent<Renderer>();
            rend.materials[2].SetColor("_EmissionColor", Color.green);
            DynamicGI.SetEmissive(rend, Color.green);
            m_completeSign.CompleteRoom();
        }
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        //Start loading scene
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < 3)
        {
            StartCoroutine(LoadBankThenScene());
        }
    }

    #region Private Functions

    void OnTriggerEnter(Collider other)
    {
        float dotToCamera = Vector3.Dot(transform.right, (other.transform.position - transform.position).normalized);
        // If the player enters the trigger from inside the room, automatically open the door.
        if (dotToCamera < 0)
        {
            m_opening = true;
            PlayDoorSound();
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Wait for door to close then unload
        StartCoroutine(WaitForDoorToOpenThenClose());
    }

    void PlayDoorSound()
    {
        FMOD.Studio.PLAYBACK_STATE playState;
        m_doorEvent.getPlaybackState(out playState);
        if (playState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            return;
        }
        m_doorEvent.start();
        m_reverbAmount.setValue(m_doorReverb);
        m_direction.setValue((m_opening ? 180 : -180));
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_doorEvent, transform, null);
    }

    IEnumerator LoadBankThenScene()
    {
        //~~~~~~~~~~~~~~~ Load the room audio ~~~~~~~~~~~~~~~\\
        if (m_bankToLoad != "")
        {
            //---------------------------------Fmod-------------------------------
            //  Start loading the bank in the backgrouond including the audio 
            //  sample data.
            //--------------------------------------------------------------------
            FMODUnity.RuntimeManager.LoadBank(m_bankToLoad, true);
            //---------------------------------Fmod-------------------------------
            //  Keep yielding the coroutine until the bank has loaded.
            //--------------------------------------------------------------------
            while (FMODUnity.RuntimeManager.AnyBankLoading())
            {
                yield return true;
            }
        }
        if (!SceneManager.GetSceneByName(m_sceneToLoad).isLoaded)
        {
            s_async = SceneManager.LoadSceneAsync(m_sceneToLoad, LoadSceneMode.Additive);
            while (!s_async.isDone)
            {
                yield return true;
            }
        }

        m_opening = true;
        PlayDoorSound();
    }

    IEnumerator WaitForDoorToOpenThenClose()
    {
        while (m_upperDoor.transform.localPosition != m_upperOpenPos)
        {
            yield return false;
        }
        yield return new WaitForSeconds(m_doorHoldTime);
        m_opening = false;

        float dotToCamera = Vector3.Dot(transform.right, (Camera.main.transform.position - transform.position).normalized);
        PlayDoorSound();

        if (dotToCamera > 0)
        {
            m_completed = true;
            // Check to see if door is closed
            while (m_lowerDoor.transform.localPosition != m_lowerClosedPos)
            {
                yield return false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
            //Add lowpass filter effect here, while door closes//
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
            //Add timer before unloading bank and add lowpass effect to imitate occlusion through closed door//
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
            yield return new WaitForSeconds(m_unloadDelay);

            if (SceneManager.GetSceneByName(m_sceneToLoad).isLoaded)
            {
                yield return new WaitForEndOfFrame();
                SceneManager.UnloadScene(m_sceneToLoad);
                if (m_bankToLoad != "")
                    FMODUnity.RuntimeManager.UnloadBank(m_bankToLoad);
            }
        }
    }

    #endregion
}
