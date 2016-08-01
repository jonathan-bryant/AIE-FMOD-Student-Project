using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {
    public int m_startingIndex = 0;
    public bool m_3D;

    void Start () {
        m_startingIndex = m_startingIndex % transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
