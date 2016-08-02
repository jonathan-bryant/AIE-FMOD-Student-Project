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

    // Private Vars
    FMOD.Sound m_sound;
    FMOD.Channel m_channel;
    FMOD.ChannelGroup m_channelGroup;
    FMOD.DSP m_fftDsp;

    bool m_isPlaying = false;

    //FMOD.RESULT result;              // Used for error checking large number of FMOD functions.

	void Start () 
	{
        m_fftArray = new float[225];
       
        m_soundPath = Application.dataPath + "/Scripts/Practical/SoundVFX/At.mp3";
        Debug.Log(m_soundPath);
        // Start by creating/initialising the sound, channel group and dsp effect's required.
        FMODUnity.RuntimeManager.LowlevelSystem.createSound(m_soundPath, FMOD.MODE.CREATESTREAM | FMOD.MODE._3D, out m_sound);
        FMODUnity.RuntimeManager.LowlevelSystem.createChannelGroup("Music Group", out m_channelGroup);
        FMODUnity.RuntimeManager.LowlevelSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out m_fftDsp);
        PlaySound();
        m_channel.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, m_fftDsp);
	}
	
	void Update () 
	{
        m_channel.isPlaying(out m_isPlaying);
        if (m_isPlaying)
        {
            IntPtr unmanagedData;
            uint length;
            m_fftDsp.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
            FMOD.DSP_PARAMETER_FFT m_fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));

            // Spectrum contains 2 channels and 2048 "bins"
            // Grab the front 112 bins from each channel (should be the same really)
            for (int bin = 0; bin < 113; bin++)
            {
                m_fftArray[bin] = Mathf.Lerp(m_fftArray[bin], m_fftData.spectrum[0][bin], 0.3f);
            }
            for (int bin = 0; bin < 112; bin++)
            {
                m_fftArray[113 + bin] = Mathf.Lerp(m_fftArray[113 + bin], m_fftData.spectrum[1][bin], 0.3f);
            }
        }
	}

	#region Private Functions

    bool PlaySound()
    {
        FMODUnity.RuntimeManager.LowlevelSystem.playSound(m_sound, m_channelGroup, false, out m_channel);
        FMOD.VECTOR pos = FMODUnity.RuntimeUtils.ToFMODVector(transform.position);
        FMOD.VECTOR vel = FMODUnity.RuntimeUtils.ToFMODVector(new Vector3(0, 0, 0));
        m_channel.set3DAttributes(ref pos,ref vel, ref vel);
        return true;
    }

	#endregion
}
