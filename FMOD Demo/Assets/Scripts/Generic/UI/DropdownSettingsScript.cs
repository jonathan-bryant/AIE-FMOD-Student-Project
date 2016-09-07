/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			24/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class DropdownSettingsScript : MonoBehaviour 
{
    public Dropdown m_qualityDropdown;
    public Dropdown m_resolutionDropdown;
    public Toggle m_fullscreenToggle;

    Resolution[] m_resolutions;

    void Start()
    {
        m_qualityDropdown.value = QualitySettings.GetQualityLevel();
        m_qualityDropdown.onValueChanged.AddListener(delegate { OnQualityValueChange(); });


        m_resolutionDropdown.ClearOptions();
        m_resolutions = Screen.resolutions;

        for (int i = 0; i < m_resolutions.Length; i++)
        {
            m_resolutionDropdown.options.Add(new Dropdown.OptionData(m_resolutions[i].ToString()));
            if (Screen.currentResolution.ToString() == m_resolutions[i].ToString())
            {
                Debug.Log("found current res");
                m_resolutionDropdown.value = i;
            }
        }
        m_resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionValueChange(); });

        m_fullscreenToggle.isOn = Screen.fullScreen;
        m_fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggleChange(); });
    }


#if UNITY_EDITOR || DEVELOPMENT_BUILD
    void OnGUI()
    {
        string[] names = QualitySettings.names;
        GUILayout.BeginVertical();
        int i = 0;
        while (i < names.Length)
        {
            if (GUILayout.Button(names[i]))
                QualitySettings.SetQualityLevel(i, true);

            i++;
        }
        GUILayout.EndVertical();
    }
#endif

    void OnQualityValueChange()
    {
        QualitySettings.SetQualityLevel(m_qualityDropdown.value);
    }

    void OnResolutionValueChange()
    {
        Screen.SetResolution(m_resolutions[m_resolutionDropdown.value].width, m_resolutions[m_resolutionDropdown.value].height, m_fullscreenToggle.isOn);
    }

    void OnFullscreenToggleChange()
    {
        Screen.fullScreen = m_fullscreenToggle.isOn;
    }
}
