﻿/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Stores the starting index.                                      |
===============================================================================================*/
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
