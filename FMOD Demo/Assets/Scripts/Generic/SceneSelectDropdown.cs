/*=================================================================
Project:		#AIE FMOD#
Developer:		#Cameron Baron#
Company:		#COMPANY#
Date:			#01/08/2016#
==================================================================*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Dropdown))]
public class SceneSelectDropdown : MonoBehaviour 
{
    // Public Vars
    public string[] m_sceneNames;

    // Private Vars
    Dropdown m_dropdown;

	void Start () 
	{
        m_dropdown = GetComponent<Dropdown>();
        m_dropdown.options.Clear();
        
        for (int i = 0; i < m_sceneNames.Length; i++)
        {
            Dropdown.OptionData temp = new Dropdown.OptionData();
            temp.text = m_sceneNames[i];
            m_dropdown.options.Add(temp);
            Debug.Log(i);
        }
	}

	#region Private Functions

	#endregion
}
