using UnityEngine;
using System.Collections;

public class SoundVFXPlayer : MonoBehaviour
{
	[FMODUnity.EventRef]
	public string m_musicRef;
	private FMOD.Studio.EventInstance m_musicEvent;   // Creating an event will allow us to manage the sound over its lifetime.
	private FMOD.Studio.EventDescription m_musicDesc;

	private string m_levelString = "Level";
	private FMOD.Studio.ParameterInstance m_level;
	[Range(0,80)]	public float m_levelValue;

	private FMODUnity.ParamRef[] m_params;

	void Start ()
	{
		m_musicEvent = FMODUnity.RuntimeManager.CreateInstance(m_musicRef);
		m_musicEvent.getParameter(m_levelString, out m_level);
		m_musicEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_musicEvent, gameObject.transform, null);

		m_musicEvent.start();
	}

	void OnDestroy()
	{
		m_musicEvent.release();
	}
	
	void Update ()
	{
		if (m_musicEvent != null)
		{
			FMOD.Studio.PLAYBACK_STATE playBackState;
			m_musicEvent.getPlaybackState(out playBackState);
			if (playBackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
			{
				m_musicEvent.release();
				m_musicEvent = null;
				SendMessage("Music Stopped!");
			}
		}

		m_musicEvent.setParameterValue(m_levelString, m_levelValue);
	}
	
	void StopAllSoundEvents()
	{
		FMOD.Studio.Bus masterBus = FMODUnity.RuntimeManager.GetBus("bus:/"); // Gets master bus.
		masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
}
