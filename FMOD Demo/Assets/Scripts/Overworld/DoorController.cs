/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class DoorController : ActionObject 
{
    // Public Vars
    public GameObject m_door;
    public string m_sceneToLoad;

    //---------------------------------Fmod-------------------------------
    //  Bank reference is the same as and Event, except using BankRef.
    //  Another difference between Events and Banks is loading and 
    //  unloading is done using the string and not a class or instance.
    //--------------------------------------------------------------------
    [FMODUnity.BankRef] public string m_bankToLoad;

    // Private Vars
    float m_distToNewPos;
    bool m_opening = false;
    Vector3 m_openPos, m_closedPos;
    SphereCollider m_collider;

    static AsyncOperation s_async;

	void Start ()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        m_closedPos = m_door.transform.localPosition;
        m_openPos = m_closedPos + (Vector3.forward * m_door.transform.localScale.z);
        m_collider = GetComponent<SphereCollider>();
        m_collider.isTrigger = true;
    }
	
	void Update () 
	{
        m_distToNewPos = Vector3.Distance(m_door.transform.localPosition, (m_opening ? m_openPos : m_closedPos));
        
        if (m_distToNewPos > 0.3f)
        {
            m_door.transform.localPosition -= m_door.transform.right * Time.deltaTime * (m_opening ? 1 : -1);
        }
        else
        {
            m_door.transform.localPosition = (m_opening ? m_openPos : m_closedPos);
        }
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
        if (dotToCamera < 0)
        {
            m_opening = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(WaitForDoorToOpenThenClose());
        //Wait for door to close then unload
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
            Debug.Log("Bank loaded");
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
            Debug.Log("Scene loaded");
        }

        m_opening = true;
        Debug.Log("Load coroutine done");
    }

    IEnumerator WaitForDoorToOpenThenClose()
    {
        while (m_door.transform.localPosition != m_openPos)
        {
            yield return false;
        }
        Debug.Log("Past while loop");
        m_opening = false;
        //
        float dotToCamera = Vector3.Dot(transform.right, (Camera.main.transform.position - transform.position).normalized);
        
        if (dotToCamera > 0)
        {
            //dotToCamera = Vector3.Dot(transform.right, (Camera.main.transform.position - transform.position).normalized);

            // Check to see if door is closed
            Debug.Log(m_distToNewPos);
            if (m_distToNewPos > 0.3f)
            {
                Debug.Log("Door not fully closed");
                //yield return false;
            }
        
        //    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        //    //Add timer before unloading bank and add lowpass effect to imitate occlusion through closed door//
        //    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        //
        //    // unload bank
        //    //if (m_bankToLoad != "")
        //    //
        //
        //    // Then unload scene
        //    Debug.Log("Unload scene");
            if (SceneManager.GetSceneByName(m_sceneToLoad).isLoaded)
            {
                SceneManager.UnloadScene(m_sceneToLoad);
                //FMODUnity.RuntimeManager.UnloadBank(m_bankToLoad);
                
            }
        }
    }

	#endregion
}
