/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			03/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour 
{
    // Public Vars

    // Private Vars
      public string m_sceneToLoad;
    int m_sceneIndex;

	void Start () 
	{
        if (m_sceneToLoad == null)
        {
            Debug.LogError("No scene attached to " + gameObject.name);
            Destroy(this);
        }
	}
	
	void Update () 
	{
	
	}

	#region Private Functions

    void OnTriggerEnter()
    {
        if (!SceneManager.GetSceneByName(m_sceneToLoad).isLoaded)
        {
            SceneManager.LoadSceneAsync(m_sceneToLoad, LoadSceneMode.Additive);
        }
    }

	#endregion
}
