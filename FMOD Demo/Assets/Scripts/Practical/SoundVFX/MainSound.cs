/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			01/08/2016
==================================================================*/

using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class MainSound : MonoBehaviour
{
    // Public Vars
    string m_soundPath;
    public float[] m_fftArray;

    public Texture2D m_soundTex;

    // Private Vars
    FMOD.Sound m_sound;
    FMOD.Channel m_channel;
    FMOD.ChannelGroup m_channelGroup;
    FMOD.DSP m_fftDsp;

    bool m_isPlaying = false;

	public int WINDOWSIZE = 1024;

    //FMOD.RESULT result;              // Used for error checking large number of FMOD functions.

    void Awake()
    {
        m_fftArray = new float[WINDOWSIZE];

        m_soundPath = Application.dataPath + "/Scripts/Practical/SoundVFX/EDM.mp3";
        Debug.Log(m_soundPath);
        // Start by creating/initialising the sound, channel group and dsp effect's required.
        FMODUnity.RuntimeManager.LowlevelSystem.createSound(m_soundPath, FMOD.MODE.CREATESTREAM | FMOD.MODE._3D, out m_sound);
        FMODUnity.RuntimeManager.LowlevelSystem.createChannelGroup("Music Group", out m_channelGroup);
        FMODUnity.RuntimeManager.LowlevelSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out m_fftDsp);
		m_fftDsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.HANNING);
		m_fftDsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WINDOWSIZE * 2);

        PlaySound();
        m_channel.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, m_fftDsp);

        m_soundTex = new Texture2D(WINDOWSIZE, 1, TextureFormat.RGB24, false);
        m_soundTex.name = "Image";
    }

    void Update()
    {
        m_channel.isPlaying(out m_isPlaying);
        if (m_isPlaying)
        {
            IntPtr unmanagedData;
            uint length;
			
            m_fftDsp.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
            FMOD.DSP_PARAMETER_FFT m_fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));

			if (m_fftData.numchannels < 1)
				return;

            // Spectrum contains 2 channels and 2048 "bins"
            // Grab the front 112 bins from each channel (should be the same really)
            for (int bin = 0; bin < WINDOWSIZE; bin++)
            {
                m_fftArray[bin] = Mathf.Lerp(m_fftArray[bin], lin2DB(m_fftData.spectrum[0][bin]), 0.3f);
                m_soundTex.SetPixel(bin, 1, new Color(m_fftArray[bin], m_fftArray[bin], m_fftArray[bin]));
            }
            //for (int bin = 0; bin < 113; bin++)
            //{
            //    m_fftArray[112 - bin] = Mathf.Lerp(m_fftArray[113 + bin], m_fftData.spectrum[1][bin], 0.3f);
            //}
            m_soundTex.Apply();

        }
    }

    #region Private Functions

    bool PlaySound()
    {
        FMODUnity.RuntimeManager.LowlevelSystem.playSound(m_sound, m_channelGroup, false, out m_channel);
        FMOD.VECTOR pos = FMODUnity.RuntimeUtils.ToFMODVector(transform.position);
        FMOD.VECTOR vel = FMODUnity.RuntimeUtils.ToFMODVector(new Vector3(0, 0, 0));
        m_channel.set3DAttributes(ref pos, ref vel, ref vel);
        m_sound.setLoopCount(1000);
        return true;
    }

    float lin2DB(float linear)
    {
        return (Mathf.Clamp(linear * 5.0f, 0.0f, 1.0f));
    }

	#endregion
}
