using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class DoorController : ActionObject
{
	//---------------------------------Fmod-------------------------------
	//  Bank reference is the same as and Event, except using BankRef.
	//  Another difference between Events and Banks is loading and 
	//  unloading is done using the string and not a class or instance.
	//--------------------------------------------------------------------
	[FMODUnity.BankRef]	public string m_bankToLoad;

	// Public Vars
	public GameObject m_door;			// Ref to door object that gets moved.
    public string m_sceneToLoad;		// Name of scene to be loaded/unloaded (needs to be exact).
	public float m_doorHoldTime;		// Amount of time the door will stay open after leaving the trigger area before closing.
	public float m_unloadDelay;         // Time until scene is unloaded once door has fully closed.
	public RoomCompleted m_completeSign;

    // Private Vars
    float m_distToNewPos;				// Distance between door and the current target position.
    bool m_opening = false;				// Bool used to determine which way the door will move (open/close).
    Vector3 m_openPos, m_closedPos;     // Positions of door (in local space) when open/closed.

	bool m_completed = false;

	static AsyncOperation s_async;

	void Start ()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;		// Setting the thread priority to low forces the async operations to use less cpu.
        m_closedPos = m_door.transform.localPosition;
        m_openPos = m_closedPos + (Vector3.forward * m_door.transform.localScale.z);
    }
	
	void Update () 
	{
        m_distToNewPos = Vector3.Distance(m_door.transform.localPosition, (m_opening ? m_openPos : m_closedPos));
        
        if (m_distToNewPos > 0.1f)
        {
            m_door.transform.localPosition -= m_door.transform.right * Time.deltaTime * (m_opening ? 1 : -1);
        }
        else
        {
            m_door.transform.localPosition = (m_opening ? m_openPos : m_closedPos);
        }

		if (m_completed)
			m_completeSign.CompleteRoom();
	}

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        //Start loading scene
        StartCoroutine(LoadBankThenScene());
    }

    #region Private Functions

    void OnTriggerEnter(Collider other)
    {
        float dotToCamera = Vector3.Dot(transform.right, (other.transform.position - transform.position).normalized);
		// If the player enters the trigger from inside the room, automatically open the door.
        if (dotToCamera < 0)
        {
            m_opening = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Wait for door to close then unload
        StartCoroutine(WaitForDoorToOpenThenClose());
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
    }

    IEnumerator WaitForDoorToOpenThenClose()
    {
        while (m_door.transform.localPosition != m_openPos)
        {
            yield return false;
        }
		yield return new WaitForSeconds(m_doorHoldTime);
        m_opening = false;

        float dotToCamera = Vector3.Dot(transform.right, (Camera.main.transform.position - transform.position).normalized);
        
        if (dotToCamera > 0)
		{
			m_completed = true;
			// Check to see if door is closed
			while (m_door.transform.localPosition != m_closedPos)
            {
                yield return false;
            }

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
