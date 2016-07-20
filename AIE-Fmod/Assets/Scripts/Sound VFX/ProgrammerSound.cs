/*=================================================================
Project:		AIE-Fmod
Developer:		Cameron Baron
Company:		FMOD
Date:			18/07/16
==================================================================*/

using System;
using UnityEngine;
using System.Runtime.InteropServices;


public class ProgrammerSound : MonoBehaviour 
{
	// Public Vars
	public string m_soundPath = "/Assets/SoundVFX/7nation.mp3";
	public FMOD.Sound m_sound;
	public FMOD.Channel m_channel;
	public FMOD.ChannelGroup m_channelGroup;
	public FMOD.DSP fft;


	// Private Vars
	private FMOD.RESULT result;

	LineRenderer lineRenderer;
	const int WindowSize = 1026;
	const float WIDTH = 10.0f;
	const float HEIGHT = 0.1f;

	#region Unity Functions

	void Start () 
	{
		result = FMODUnity.RuntimeManager.LowlevelSystem.createChannelGroup("Music Group", out m_channelGroup);
		result = FMODUnity.RuntimeManager.LowlevelSystem.createSound(m_soundPath, FMOD.MODE.CREATESTREAM | FMOD.MODE._3D, out m_sound);

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetVertexCount(WindowSize);
		lineRenderer.SetWidth(.1f, .1f);

		FMODUnity.RuntimeManager.LowlevelSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out fft);
		fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.HANNING);
		fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WindowSize * 2);
		

		if (result == FMOD.RESULT.OK)
		{
			PlaySound();
			m_channel.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, fft);
		}
	}
	
	void Update () 
	{
		IntPtr unmanagedData;
		uint length;
		fft.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
		FMOD.DSP_PARAMETER_FFT fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));
		var spectrum = fftData.spectrum;

		if (fftData.numchannels > 0)
		{
			var pos = Vector3.zero;
			pos.z = 5.0f;
			pos.x = WIDTH * -0.5f;

			for (int i = 0; i < WindowSize; ++i)
			{
				pos.x += (WIDTH / WindowSize);

				float level = lin2dB(spectrum[0][i]);
				pos.y = (level + 80) * HEIGHT;

				lineRenderer.SetPosition(i, pos);
			}
		}
	}

	#endregion

	public bool PlaySound()
	{
		result = FMODUnity.RuntimeManager.LowlevelSystem.playSound(m_sound, m_channelGroup, false, out m_channel);

		if (result != FMOD.RESULT.OK)
			return false;

		return true;
	}

	#region Private Functions

	float lin2dB(float linear)
	{
		return Mathf.Clamp(Mathf.Log10(linear) * 20.0f, -80.0f, 0.0f);
	}

	#endregion
}
