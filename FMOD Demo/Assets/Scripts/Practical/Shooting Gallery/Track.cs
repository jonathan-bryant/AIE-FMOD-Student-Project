/*===============================================================================================
|  Project:		FMOD Demo                                                                       |
|  Developer:	Matthew Zelenko                                                                 |
|  Company:		FMOD                                                                            |
|  Date:		20/09/2016                                                                      |
================================================================================================*/
using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {
    public int m_startingIndex = 0;

    void Start () {
        m_startingIndex = m_startingIndex % transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
