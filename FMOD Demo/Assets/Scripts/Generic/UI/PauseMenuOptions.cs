/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			06/09/2016
==================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class PauseMenuOptions : MonoBehaviour 
{
    // Public Vars
    public Slider m_masterSlider;
    public Slider m_effectsSlider;
    public Slider m_dialogueSlider;

    float m_masterLevel, m_effectsLevel, m_dialogueLevel;

	// Private Vars

	void Start () 
	{
        // Get reference to Master Bus
        FMODUnity.RuntimeManager.GetBus("");
        // Get reference to Effects Bus

        // Get reference to Dialogue Bus


        m_masterSlider.onValueChanged.AddListener(delegate { OnMasterValueChange(); });
        m_effectsSlider.onValueChanged.AddListener(delegate { OnEffectsValueChange(); });
        m_dialogueSlider.onValueChanged.AddListener(delegate { OnDialogueValueChange(); });
	}
    
	#region Private Functions

    void OnMasterValueChange()
    {

    }
    void OnEffectsValueChange()
    {

    }
    void OnDialogueValueChange()
    {

    }

    #endregion
}
