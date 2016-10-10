/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                10/10/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Musical note particle emitter.                                  |
===============================================================================================*/

using UnityEngine;


public class MusicNoteEmitter : MonoBehaviour 
{
    // Public Vars
    public bool m_runEmitter = true;       // On/off switch
    public GameObject[] m_notes;
    public float m_spawnTime;
    public Vector3 m_direction;
    public bool m_useForward = true;

    public float m_minSize = 0.5f;
    public float m_maxSize = 1;

    // Private Vars
    float m_timer;

	void Start() 
	{
	    if (m_notes.Length < 1)
        {
            Debug.Log("No notes to spawn!");
            Destroy(this);
        }

        if (m_useForward)
        {
            m_direction = transform.forward;
        }
	}
	
	void Update() 
	{
        if (m_runEmitter)
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_spawnTime)
            {
                CreateNewNote(Random.Range(0, 3));
                m_timer = 0.0f;
            }
        }
	}

    public void CreateNewNote(int index)
    {
        GameObject note = Instantiate(m_notes[index], transform.position, Quaternion.identity) as GameObject;
        note.transform.localScale *= Random.Range(m_minSize, m_maxSize);
        note.GetComponent<Rigidbody>().AddForce(m_direction * 10);
    }

    #region Private Functions


    #endregion
}
