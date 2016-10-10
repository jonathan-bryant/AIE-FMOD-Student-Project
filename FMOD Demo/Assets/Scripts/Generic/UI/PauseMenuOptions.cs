/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                06/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Controls the bus volumes using sliders in the main menu.        |
===============================================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class PauseMenuOptions : MonoBehaviour 
{
    // Public Vars
    public Slider m_masterSlider, m_effectsSlider, m_dialogueSlider;
    /*===============================================Fmod====================================================
    |       Names of the buses we want to control: the will all start with "Bus:/" which refers to the      |
    |                                             Master Bus.                                               |
    =======================================================================================================*/
    public string m_masterBusPath = "Bus:/";
    public string m_effectsBusPath = "Bus:/SFX";
    public string m_dialogueBusPath = "Bus:/VO";

	// Private Vars
    /*===============================================Fmod====================================================
    |                                  Buses are a FMOD Studio variable.                                    |
    =======================================================================================================*/
    FMOD.Studio.Bus m_masterBus, m_effectsBus, m_dialogueBus;
    float m_masterLevel, m_effectsLevel, m_dialogueLevel;


	void Start () 
	{
        /*===============================================Fmod====================================================
        |             Set the reference to the Master bus and adjust the slider to match the level.             |
        |   Adding a listener in Unity will allow us to update the bus value when the sliders value is changed. |
        =======================================================================================================*/
        m_masterBus = FMODUnity.RuntimeManager.GetBus(m_masterBusPath);
        m_masterBus.getFaderLevel(out m_masterLevel);
        m_masterSlider.value = m_masterLevel;
        m_masterSlider.onValueChanged.AddListener(delegate { OnMasterValueChange(); });

        /*===============================================Fmod====================================================
        |                       Repeat the process for any other bus you wish to control.                       |
        |                                   Otherwise disable the slider.                                       |
        =======================================================================================================*/
        if (m_effectsBusPath != "")
        {
            m_effectsBus = FMODUnity.RuntimeManager.GetBus(m_effectsBusPath);
            m_effectsBus.getFaderLevel(out m_effectsLevel);
            m_effectsSlider.value = m_effectsLevel;
            m_effectsSlider.onValueChanged.AddListener(delegate { OnEffectsValueChange(); });
        }
        else
            m_effectsSlider.enabled = false;

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

        /*===============================================Fmod====================================================
        |                    When the slider changes value, set teh bus to the same value.                      |
        =======================================================================================================*/

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
