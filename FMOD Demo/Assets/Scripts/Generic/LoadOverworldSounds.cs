/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			16/08/2016
==================================================================*/

using UnityEngine;


public class LoadOverworldSounds : MonoBehaviour 
{
    // Public Vars
    [FMODUnity.BankRef]
    public string m_bankString;

    // Private Vars
    FMOD.Studio.System m_studioSystem;

	void Start () 
	{
        FMODUnity.RuntimeManager.StudioSystem.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, (System.IntPtr)0);
        FMODUnity.RuntimeManager.LoadBank(m_bankString, true);
	}

    void OnDestroy()
    {
        FMODUnity.RuntimeManager.UnloadBank(m_bankString);
    }
}
