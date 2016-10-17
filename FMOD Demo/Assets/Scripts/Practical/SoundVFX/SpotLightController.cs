/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;


public class SpotLightController : MonoBehaviour 
{
    // Public Vars
    public MainSound m_soundRef;

	// Private Vars
    RoofLighting[] m_lights;
    int m_lastBeat = -1;

	void Start () 
	{
        m_lights = GetComponentsInChildren<RoofLighting>();
        if (m_lights.Length < 1 || m_soundRef == null)
        {
            Debug.Log("Missing the lights or sound!");
            Destroy(this);
        }
	}
	
	void Update () 
	{
        int beat = m_soundRef.m_timelineInfo.beat;
        if (m_soundRef.m_timelineInfo.tempo > 140 && m_lastBeat != beat)
        {
            ActivateRandomLight();
            m_lastBeat = beat;
        }
	}

	public void ActivateRandomLight()
    {
        m_lights[Random.Range(0, m_lights.Length)].TurnOnLight();
    }
}
