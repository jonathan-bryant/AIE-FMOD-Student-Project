/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                10/10/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Musical note controller.                                        |
===============================================================================================*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MusicNote : MonoBehaviour 
{
    // Public Vars
    public float m_lifeTime;

    // Private Vars
    float m_timer;

	void Start () 
	{
	
	}
	
	void Update () 
	{
        m_timer += Time.deltaTime;
        if (m_timer > m_lifeTime)
        {
            Destroy(gameObject);
        }

        Vector3 temp = new Vector3(0, Mathf.Sin(Time.fixedDeltaTime / 10), 0);
        transform.position += temp;
	}

	#region Private Functions

	#endregion
}
