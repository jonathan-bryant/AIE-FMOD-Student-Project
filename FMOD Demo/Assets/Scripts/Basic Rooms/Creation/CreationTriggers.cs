/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			02/08/2016
==================================================================*/

using UnityEngine;


public class CreationTriggers : MonoBehaviour 
{
    // Public Vars
    [FMODUnity.EventRef]
    public string m_soundRef = "";

    // Private Vars
    FMOD.Studio.EventInstance m_soundInstance;
    FMOD.Studio.ParameterInstance m_soundIntensity;
    float m_instensity;
    FMOD.Studio.PLAYBACK_STATE m_playState;
    bool m_isPaused = false;

	void Start () 
	{
        m_soundInstance = FMODUnity.RuntimeManager.CreateInstance(m_soundRef);
        m_soundInstance.getParameter("Intensity", out m_soundIntensity);
        Play();
	}
	
	void Update ()
    {
        m_soundInstance.getPlaybackState(out m_playState);      // Update the current playstate of the sound.
        
    }

    public void Play()
    {
        m_soundInstance.getPaused(out m_isPaused);

        if (m_playState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            m_instensity = 0.0f;
            m_soundInstance.setParameterValue("intensity", m_instensity);
            m_soundInstance.start();
        }
        else if (m_isPaused)
        {
            m_soundInstance.setPaused(false);
        }
    }

    public void Pause()
    {
        if (!m_isPaused)
        {
            m_soundInstance.setPaused(true);
            m_isPaused = true;
        }
    }

    public void Stop()
    {
        m_instensity = 0.8f;
        m_soundInstance.setParameterValue("Intensity", m_instensity);        
    }

    public void Kill()
    {
        m_soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

	#region Private Functions

	#endregion
}
