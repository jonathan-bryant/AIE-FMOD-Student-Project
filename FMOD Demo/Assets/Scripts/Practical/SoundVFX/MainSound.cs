/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			01/08/2016
==================================================================*/

using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class MainSound : MonoBehaviour 
{
    // Public Vars
    [FMODUnity.EventRef]
    public string m_soundPath = "";
    public FMOD.DSP_PARAMETER_FFT m_fftData { get { return m_fftData; } private set { m_fftData = value; } }

    // Private Vars
    FMOD.Sound m_sound;
    FMOD.Channel m_channel;
    FMOD.ChannelGroup m_channelGroup;
    FMOD.DSP m_fftDsp;

    bool m_isPlaying = false;

    // FMOD.RESULT result;              // Used for error checking large number of FMOD functions.

	void Start () 
	{
        // Start by creating/initialising the sound, channel group and dsp effect's required.
        FMODUnity.RuntimeManager.LowlevelSystem.createSound(m_soundPath, FMOD.MODE.CREATESTREAM | FMOD.MODE._3D, out m_sound);
        FMODUnity.RuntimeManager.LowlevelSystem.createChannelGroup("Music Group", out m_channelGroup);
        FMODUnity.RuntimeManager.LowlevelSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out m_fftDsp);

        PlaySound();
	}
	
	void Update () 
	{
        m_channel.isPlaying(out m_isPlaying);
        if (m_isPlaying)
        {
            IntPtr unmanagedData;
            uint length;
            m_fftDsp.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
            m_fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));
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
