/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			01/08/2016
==================================================================*/

using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class MainSound : MonoBehaviour
{
	// Public Vars
	string m_soundPath;

	public float[] m_fftArray;

	[HideInInspector]
	public Texture2D m_soundTex;

	// Private Vars
	[FMODUnity.EventRef]
	public string m_eventRef;
	FMOD.Studio.EventInstance m_eventInstance;

	FMOD.Sound m_sound;
	FMOD.Channel m_channel;
	FMOD.ChannelGroup m_channelGroup;
	FMOD.DSP m_fftDsp;

	bool m_isPlaying = false;
	bool m_dspAdded = false;

	int WINDOWSIZE = 1024;

	FMOD.RESULT result;              // Used for error checking large number of FMOD functions.

	void Awake()
	{
		m_fftArray = new float[WINDOWSIZE];
		m_soundTex = new Texture2D(WINDOWSIZE, 1, TextureFormat.RGB24, false);
		m_soundTex.name = "Image";
		m_soundTex.wrapMode = TextureWrapMode.Clamp;

		// Start by creating/initialising the sound, channel group and dsp effect's required.
		m_eventInstance = FMODUnity.RuntimeManager.CreateInstance(m_eventRef);
		PlaySound();

		StartCoroutine(AddDspToChannel());
	}

	void Update()
	{
		if (m_channelGroup != null)
			m_channelGroup.isPlaying(out m_isPlaying);
		if (m_isPlaying && m_dspAdded)
		{
			IntPtr unmanagedData;
			uint length;

			m_fftDsp.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
			FMOD.DSP_PARAMETER_FFT m_fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));

			if (m_fftData.numchannels < 1)
				return;

			// Spectrum contains 2 channels and 2048 "bins" by default
			for (int bin = 0; bin < WINDOWSIZE; bin++)
			{
				float temp = lin2DB(m_fftData.spectrum[0][bin]);
				temp = ((temp + 80.0f) * (1 / 80.0f));
				m_fftArray[bin] = Mathf.Lerp(m_fftArray[bin], temp, 0.6f);
				m_soundTex.SetPixel(bin, 1, new Color(m_fftArray[bin], m_fftArray[bin], m_fftArray[bin]));
			}
			m_soundTex.Apply();

		}
	}

	#region Private Functions

	bool PlaySound()
	{
		m_eventInstance.start();
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_eventInstance, transform, null);
		return true;
	}

	float lin2DB(float linear)
	{
		return (Mathf.Clamp(Mathf.Log10(linear) * 20, -80.0f, 0.0f));
	}

	IEnumerator AddDspToChannel()
	{
		/*--------------------------------FMOD------------------------------
		//
		//		Before adding a dsp effect to the channel group, we need
		//		to make sure the event is playing, otherwise we get an error
		//		because the channel isn't set correctly yet.
		//
		//----------------------------------------------------------------*/
		while (m_channelGroup == null)
		{
			result = m_eventInstance.getChannelGroup(out m_channelGroup);
			yield return null;
		}

		FMODUnity.RuntimeManager.LowlevelSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out m_fftDsp);
		m_fftDsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.HANNING);
		m_fftDsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WINDOWSIZE * 2);

		m_channelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, m_fftDsp);
		m_dspAdded = true;
	}

	#endregion
}
