/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Changes door sprite from incomplete to complete                 |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class RoomCompleted : MonoBehaviour {

    public Sprite m_completed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void CompleteRoom()
    {
        GetComponent<SpriteRenderer>().sprite = m_completed;
    }
}
