/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			03/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadRoomsTrigger : MonoBehaviour
{
    // Public Vars

    // Private Vars
    int m_sceneCount;

    void Start()
    {

    }

    #region Private Functions

    void OnTriggerEnter()
    {
        m_sceneCount = SceneManager.sceneCount;
        for (int index = 0; index < m_sceneCount; index++)
        {
            Scene indexScene = SceneManager.GetSceneAt(index);
            if (indexScene.isLoaded && indexScene.name != "Overworld")
            {
                Debug.Log("Unloading: " + indexScene.name);
                //SceneManager.UnloadScene(indexScene.name);    // Breaks computer (@Fmod, possibly bad pc's)
            }
        }
    }

    #endregion
}
