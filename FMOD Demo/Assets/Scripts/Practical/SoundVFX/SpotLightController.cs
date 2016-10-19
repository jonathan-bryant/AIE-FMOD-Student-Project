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
    public GameObject[] m_signObjects;
    public GameObject[] m_wallObjects;
    public RoofLighting[] m_lights;

	// Private Vars
    int m_lastBeat = -1;

	void Start () 
	{
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

        int index = Random.Range(0, 5);

        for (int i = 0; i < m_wallObjects.Length; i++)
        {
            Renderer rend = m_wallObjects[i].GetComponent<Renderer>();
            rend.materials[1].SetFloat("_EmissionAmount", m_soundRef.m_fftArray[index] * 5);
        }
        for (int i = 0; i < m_signObjects.Length; i++)
        {
            Renderer rend = m_signObjects[i].GetComponent<Renderer>();
            rend.material.SetFloat("_EmissionAmount", m_soundRef.m_fftArray[index] * 5);
        }
    }

	public void ActivateRandomLight()
    {
        int index = Random.Range(0, m_lights.Length);
        m_lights[index].TurnOnLight();

        int index2 = Random.Range(0, m_lights.Length);
        while (index == index2)
        {
            index2 = Random.Range(0, m_lights.Length);
        }
        m_lights[index2].TurnOnLight();
    }
}
