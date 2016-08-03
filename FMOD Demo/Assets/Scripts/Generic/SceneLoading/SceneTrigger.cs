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

    // Private Vars
    private static AsyncOperation m_async;
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
        Scene temp = SceneManager.GetSceneByName(m_sceneToLoad);
        if (!temp.isLoaded)
        {
            StartCoroutine(LoadScene(m_sceneToLoad));
        }
    }

    IEnumerator LoadScene(string a_sceneName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync(m_sceneToLoad, LoadSceneMode.Additive);
    }

	#endregion
}
