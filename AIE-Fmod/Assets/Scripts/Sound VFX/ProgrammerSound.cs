/*=================================================================
Project:		AIE-Fmod
Developer:		Cameron Baron
Company:		FMOD
Date:			18/07/16
==================================================================*/

using UnityEngine;


public class ProgrammerSound : MonoBehaviour 
{
	// Public Vars
	public string m_soundPath = "/Assets/SoundVFX/7nation.mp3";
	public FMOD.Sound m_sound;
	public FMOD.Channel m_channel;
	public FMOD.ChannelGroup m_channelGroup;

	// Private Vars
	private FMOD.RESULT result;

	#region Unity Functions

	void Start () 
	{
		result = FMODUnity.RuntimeManager.LowlevelSystem.createChannelGroup("Music Group", out m_channelGroup);
		result = FMODUnity.RuntimeManager.LowlevelSystem.createSound(m_soundPath, FMOD.MODE.CREATESTREAM | FMOD.MODE._3D, out m_sound);
		
		Debug.Log("Create Sound: " + result);

		if (result == FMOD.RESULT.OK)
		{
			PlaySound();
		}
	}
	
	void Update () 
	{
		
	}

	#endregion

	public bool PlaySound()
	{
		result = FMODUnity.RuntimeManager.LowlevelSystem.playSound(m_sound, m_channelGroup, false, out m_channel);
        FMOD.VECTOR vec = FMODUnity.RuntimeUtils.ToFMODVector(new Vector3(0.0f, 0.0f, 0.0f));
        FMOD.VECTOR pos = FMODUnity.RuntimeUtils.ToFMODVector(transform.position);
        m_channel.set3DAttributes(ref pos, ref vec, ref vec);

		if (result != FMOD.RESULT.OK)
			return false;

		return true;
	}

	#region Private Functions

	#endregion
}
