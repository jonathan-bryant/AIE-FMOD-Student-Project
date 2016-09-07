/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
    static string s_currentScene;
    static string s_previousScene;
    

	void Start ()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }
	
	void Update () 
	{
        
    }

	#region Private Functions

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player")) return;

        
    }

    void OnTriggerExit(Collider col)
    {
        if (!col.CompareTag("Player")) return;


    }

    void OnTriggerStay(Collider col)
    {
        if (!col.CompareTag("Player")) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F pressed");
            //~~~~~~~~~~~~~~~ Load this room ~~~~~~~~~~~~~~~\\
            s_previousScene = s_currentScene;
            s_currentScene = m_sceneToLoad;
            StartCoroutine(LoadSceneOpenDoor());

            //~~~~~~~~~~~~~~~ Unload all rooms, except for Overworld ~~~~~~~~~~~~~~~\\
            SceneManager.UnloadScene(s_previousScene);
        }
    }

    IEnumerator LoadSceneOpenDoor()
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

        Debug.Log("Load Scene at " + Time.time);
        s_async = SceneManager.LoadSceneAsync(m_sceneToLoad, LoadSceneMode.Additive);
        while (!s_async.isDone)
        {
            yield return true;
        }

        // When loading is done, open door
        if (m_door != null)
        {
            while (m_door.transform.localPosition.z < 2.0f)
            {
                m_door.transform.position += m_door.transform.forward * Time.deltaTime;
                yield return true;
            }
        }
    }
    

	#endregion
}
