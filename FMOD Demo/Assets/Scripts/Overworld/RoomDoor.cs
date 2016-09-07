/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			07/09/2016
==================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class RoomDoor : MonoBehaviour 
{
    // Public Vars
    public GameObject m_door;
    public string m_sceneToLoad;
    public string m_useKey;

    //---------------------------------Fmod-------------------------------
    //  Bank reference is the same as and Event, except using BankRef.
    //  Another difference between Events and Banks is loading and 
    //  unloading is done usig the string and not a class or instance.
    //--------------------------------------------------------------------
    [FMODUnity.BankRef]    public string m_bankToload;

    // Private Vars

    static AsyncOperation s_async;

    bool m_entering = false, m_entered = false;
    Vector3 doorOpenPos, doorClosedPos;
    SphereCollider m_collider;

	void Start ()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        doorClosedPos = m_door.transform.position;
        doorOpenPos = doorClosedPos + new Vector3(0, 0, 2);

        m_collider = GetComponent<SphereCollider>();
        m_collider.center = new Vector3(0, 1.5f, -0.8f);
        m_collider.radius = 2.5f;
        m_collider.isTrigger = true;
    }
	
	void Update () 
	{
        if (Input.GetKeyDown(m_useKey) && m_entering)
        {
            //~~~~~~~~~~~~~~~ Load this room ~~~~~~~~~~~~~~~\\
            if (!(m_entering && m_entered))
                StartCoroutine(LoadSceneOpenDoor());
        }
    }

	#region Private Functions

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player")) return;

        float playerDir = Vector3.Dot(transform.right, col.transform.position - transform.position);
        if (playerDir > 0)
        {
            m_entering = true;
            m_entered = false;
        }
        else
        {
            m_entering = false;
            StartCoroutine( LoadSceneOpenDoor());
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (!col.CompareTag("Player")) return;

        float playerDir = Vector3.Dot(transform.right, col.transform.position - transform.position);

        if (playerDir > 0)
        {
            m_entering = false;
            m_entered = false;
        }
        else
        {
            m_entered = true;
        }

        StartCoroutine(CloseDoor());
    }

    IEnumerator LoadSceneOpenDoor()
    {
        if (m_entering && !m_entered)
        {
            //~~~~~~~~~~~~~~~ Load the room audio ~~~~~~~~~~~~~~~\\
            if (m_bankToload != "")
            {
                //---------------------------------Fmod-------------------------------
                //  Start loading the bank in the backgrouond including the audio 
                //  sample data.
                //--------------------------------------------------------------------
                FMODUnity.RuntimeManager.LoadBank(m_bankToload, true);

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
        }
        // When loading is done, open door
        if (m_door != null)
        {
            while (m_door.transform.localPosition.z < 1.9f)
            {
                m_door.transform.position += m_door.transform.forward * Time.deltaTime;
                yield return true;
            }
        }
    }
    
    IEnumerator CloseDoor()
    {
        if (m_door != null)
        {
            while (m_door.transform.localPosition.z > 0.1f)
            {
                m_door.transform.position -= m_door.transform.forward * Time.deltaTime;
                yield return true;
            }
        }

        if (!m_entering && !m_entered)
        {
            SceneManager.UnloadScene(m_sceneToLoad);
            FMODUnity.RuntimeManager.UnloadBank(m_bankToload);
        }
    }

	#endregion
}
