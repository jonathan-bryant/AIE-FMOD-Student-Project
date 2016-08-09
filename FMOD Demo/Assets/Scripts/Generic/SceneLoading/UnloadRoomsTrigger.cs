/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			03/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UnloadRoomsTrigger : MonoBehaviour
{
    int m_sceneCount;

    void Start()
    {
        m_sceneCount = SceneManager.sceneCount;
        for (int index = 0; index < m_sceneCount; index++)
        {
            Scene indexScene = SceneManager.GetSceneAt(index);
        }
    }

    #region Private Functions

    void OnTriggerEnter()
    {
        m_sceneCount = SceneManager.sceneCount;
        Debug.Log(m_sceneCount);
        for (int index = 0; index < m_sceneCount; index++)
        {
            Scene indexScene = SceneManager.GetSceneAt(index);
            if (indexScene.isLoaded && indexScene.name != "Overworld")
            {
                Debug.Log("Unloading: " + indexScene.name);
                StartCoroutine(UnloadScene(indexScene.name));
            }
        }
    }

    IEnumerator UnloadScene(string a_sceneName)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.UnloadScene(a_sceneName);
    }

    #endregion
}
