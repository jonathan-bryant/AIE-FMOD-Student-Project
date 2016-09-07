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
    public Slider m_masterSlider, m_effectsSlider, m_dialogueSlider;
    public string m_masterBusPath = "Bus:/";
    public string m_effectsBusPath = "Bus:/SFX";
    public string m_dialogueBusPath = "Bus:/VO";

	// Private Vars
    FMOD.Studio.Bus m_masterBus, m_effectsBus, m_dialogueBus;
    float m_masterLevel, m_effectsLevel, m_dialogueLevel;


	void Start () 
	{
        // Get reference to Master Bus
        // Master bus can be a blank string and it will get the 
        m_masterBus = FMODUnity.RuntimeManager.GetBus(m_masterBusPath);
        m_masterBus.getFaderLevel(out m_masterLevel);
        m_masterSlider.value = m_masterLevel;
        m_masterSlider.onValueChanged.AddListener(delegate { OnMasterValueChange(); });

        // Get reference to Effects Bus
        if (m_effectsBusPath != "")
        {
            m_effectsBus = FMODUnity.RuntimeManager.GetBus(m_effectsBusPath);
            m_effectsBus.getFaderLevel(out m_effectsLevel);
            m_effectsSlider.value = m_effectsLevel;
            m_effectsSlider.onValueChanged.AddListener(delegate { OnEffectsValueChange(); });
        }
        else
            m_effectsSlider.enabled = false;

        // Get reference to Dialogue Bus
        if (m_dialogueBusPath != "")
        {
            m_dialogueBus = FMODUnity.RuntimeManager.GetBus(m_dialogueBusPath);
            m_dialogueBus.getFaderLevel(out m_dialogueLevel);
            m_dialogueSlider.value = m_dialogueLevel;
            m_dialogueSlider.onValueChanged.AddListener(delegate { OnDialogueValueChange(); });
        }
        else
            m_dialogueSlider.enabled = false;

	}
    
	#region Private Functions

    void OnMasterValueChange()
    {
        m_masterLevel = m_masterSlider.value;
        m_masterBus.setFaderLevel(m_masterLevel);
    }
    void OnEffectsValueChange()
    {
        m_effectsLevel = m_effectsSlider.value;
        m_effectsBus.setFaderLevel(m_effectsLevel);
    }
    void OnDialogueValueChange()
    {
        m_dialogueLevel = m_dialogueSlider.value;
        m_dialogueBus.setFaderLevel(m_dialogueLevel);
    }

    #endregion
}
