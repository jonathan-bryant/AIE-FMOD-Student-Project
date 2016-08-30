/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;


public class GrenadeScript : MonoBehaviour 
{
    // Public Vars
    [FMODUnity.EventRef]
    public string m_eventString;

    [FMODUnity.EventRef]
    public string m_snapshotString;

    public float m_snapshotRampUpTime = 1.0f;
    public float m_snapshotLength = 5.0f;
    public float m_snapshotRampDownTime = 1.0f;

    // Private Vars
    FMOD.Studio.EventInstance m_snapshotEvent;
    FMOD.Studio.ParameterInstance m_snapshotIntensity;
    float m_intensity = 0.0f;
    bool m_startSnapshot = false;
    float m_timer = 0.0f;


    void Start () 
	{
        m_snapshotEvent = FMODUnity.RuntimeManager.CreateInstance(m_snapshotString);
        m_snapshotEvent.start();
        m_snapshotEvent.getParameter("Intensity", out m_snapshotIntensity);
        m_snapshotIntensity.setValue(m_intensity);
	}
	
	void Update () 
	{
	    if (m_startSnapshot)
        {
            m_timer += Time.deltaTime;
            if (m_timer <= m_snapshotRampUpTime)
            {
                m_snapshotIntensity.setValue( (1 / m_snapshotRampUpTime) * m_timer * 100);
                m_snapshotIntensity.getValue(out m_intensity);
            }
            else if (m_timer > m_snapshotLength)
            {
                m_snapshotIntensity.setValue((1 / m_snapshotRampUpTime) * m_snapshotRampDownTime * 100);
                m_snapshotRampDownTime -= Time.deltaTime;                
            }

            if (m_snapshotRampDownTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
	}

    #region Private Functions

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(m_eventString, gameObject);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            m_startSnapshot = true;
        }
    }

    #endregion
}
