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
    int m_lastMusicBar = -1;

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
	    if (m_soundRef.m_timelineInfo.currentMusicBar != m_lastMusicBar)
        {
            ActivateRandomLight();
            m_lastMusicBar = m_soundRef.m_timelineInfo.currentMusicBar;
        }
	}

	public void ActivateRandomLight()
    {
        m_lights[Random.Range(0, m_lights.Length)].TurnOnLight();
    }
}
