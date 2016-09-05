/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			03/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTrigger : MonoBehaviour 
{
    // Public Vars
    public string m_sceneToLoad;
    public GameObject m_door;
    //---------------------------------Fmod-------------------------------
    //  Bank reference is the same as and Event, except using BankRef.
    //  Another difference between Events and Banks is loading and 
    //  unloading is done usig the string and not a class or instance.
    //--------------------------------------------------------------------
    [FMODUnity.BankRef]    public string m_bankString;

    public float m_unloadWaitTime = 3.0f;

    // Private Vars
    float m_doorSizeZ;
    Vector3 m_doorDefaultPos;

    static AsyncOperation m_async;
    int m_sceneIndex;

	void Start () 
	{
        Application.backgroundLoadingPriority = ThreadPriority.Low;

        if (m_door != null)
        {
            m_doorDefaultPos = m_door.transform.position;
            m_doorSizeZ = m_doorDefaultPos.z - (m_door.transform.localScale.z * 2);
        }

        if (m_sceneToLoad == null)
        {
            Debug.LogError("No scene attached to " + gameObject.name);
            Destroy(this);
        }
	}

	#region Private Functions

    public void LoadRoom()
    {
        Scene temp = SceneManager.GetSceneByName(m_sceneToLoad);
        if (!temp.isLoaded)
        {
            //Entering
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        if (m_bankString != "")
        {
            //---------------------------------Fmod-------------------------------
            //  Start loading the bank in the backgrouond including the audio 
            //  sample data.
            //--------------------------------------------------------------------
            FMODUnity.RuntimeManager.LoadBank(m_bankString, true);

            //---------------------------------Fmod-------------------------------
            //  Keep yielding the coroutine until the bank has loaded.
            //--------------------------------------------------------------------
            while (FMODUnity.RuntimeManager.AnyBankLoading())
            {
                yield return m_async;
            }
        }
        // For some reason Unity has issues with loading a scene the same 
        // frame as a trigger response, so delay a framw with yield return null.
        m_async = SceneManager.LoadSceneAsync(m_sceneToLoad, LoadSceneMode.Additive);
        // Not all triggers may have a door attached to it.
        if (m_door != null)
            StartCoroutine(WaitAndOpenDoor());
    }

    IEnumerator WaitAndOpenDoor()
    {
        // Put the door opening stuff here!
        while (m_async.progress < 0.9f)
        {
            yield return null;
        }

        Vector3 temp = m_doorDefaultPos;
        while (m_door.transform.position.z > m_doorSizeZ)
        {
            temp.z = m_doorSizeZ;
            m_door.transform.position += m_door.transform.forward * Time.deltaTime;
            // Play door opening sound
            yield return m_async;
        }
    }

    public void UnloadRoom()
    {
        if (SceneManager.GetSceneByName(m_sceneToLoad).isLoaded)
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor()
    {
        if (m_door != null)
        {
            while (m_door.transform.position.z <= m_doorDefaultPos.z - 0.1f)
            {
                m_door.transform.position -= m_door.transform.forward * Time.deltaTime;
                yield return m_async;
            }
        }
        StartCoroutine(UnloadScene());
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(m_unloadWaitTime);

        FMODUnity.RuntimeManager.UnloadBank(m_bankString);

        SceneManager.UnloadScene(m_sceneToLoad);
    }

    #endregion
}
