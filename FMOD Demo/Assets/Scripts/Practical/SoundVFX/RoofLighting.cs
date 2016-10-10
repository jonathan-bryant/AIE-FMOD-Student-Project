/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                03/10/2016                                                      |
|   Scene:                      Sound VFX                                                       |
|   Fmod Related Scripting:                                                                   |
|   Description:                                                  |
===============================================================================================*/

using UnityEngine;


public class RoofLighting : MonoBehaviour 
{
    // Public Vars
    public Color m_lightColor;

    // Private Vars
    Light m_lightRef;
    Material m_materialRef;

	void OnEnable () 
	{
        m_materialRef = GetComponentInChildren<Renderer>().sharedMaterials[1];
        m_lightRef = GetComponent<Light>();
	}
	
	void Update () 
	{
	
	}

	#region Private Functions

    void OnValidate()
    {
        m_materialRef = GetComponentInChildren<Renderer>().sharedMaterials[1];
        m_lightRef = GetComponent<Light>();

        m_materialRef.SetColor("_EmissionColor", m_lightColor);
        m_lightRef.color = m_lightColor;
    }

	#endregion
}
