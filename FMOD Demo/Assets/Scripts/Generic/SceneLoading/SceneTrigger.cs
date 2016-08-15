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

    // Private Vars
    float m_doorSizeZ;
    Vector3 m_doorDefaultPos;

    static AsyncOperation m_async;
    int m_sceneIndex;
    bool m_opening = false;

	void Start () 
	{
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

    void Update()
    {
        if (m_door != null)
        {
            if (m_opening && m_door.transform.position.z > m_doorSizeZ)
            {
                Vector3 temp = m_doorDefaultPos;
                temp.z = m_doorSizeZ;
                m_door.transform.position = Vector3.Lerp(m_door.transform.position, temp, 0.1f);
                // Play door opening sound
            }
            else if (!m_opening && m_door.transform.position.z < m_doorDefaultPos.z)
            {
                m_door.transform.position = Vector3.Lerp(m_door.transform.position, m_doorDefaultPos, 0.1f);
            }
        }
    }

	#region Private Functions

    void OnTriggerEnter()
    {
        Scene temp = SceneManager.GetSceneByName(m_sceneToLoad);
        if (!temp.isLoaded)
        {
            //Entering
            StartCoroutine(LoadScene(m_sceneToLoad));
        }
        else
        {
            // Exiting
            m_opening = false;
        }
    }

    IEnumerator LoadScene(string a_sceneName)
    {
        yield return new WaitForSeconds(0.1f);
        m_async = SceneManager.LoadSceneAsync(m_sceneToLoad, LoadSceneMode.Additive);
        // Not all triggers may have a door attached to it.
        if (m_door != null)
            StartCoroutine(WaitAndOpenDoor());
    }

    IEnumerator WaitAndOpenDoor()
    {
        yield return m_async;
        // Put the door opening stuff here!
        m_opening = true;
    }
	#endregion
}
