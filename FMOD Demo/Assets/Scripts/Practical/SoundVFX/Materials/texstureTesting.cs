/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;

public enum SHADER
{
    BAR_VIS,
    LINE_VIS,
}

public class texstureTesting : MonoBehaviour 
{
    // Public Vars
    public SHADER shaderType;

    // Private Vars
    [SerializeField]    Color m_color;
    MainSound soundRef;
    string shaderName;
    Material m_mat;

	void Start ()
    {
        soundRef = FindObjectOfType<MainSound>();
        if (soundRef == null)
        {
            Debug.LogError("No MainSound in scene!!!");
            DestroyImmediate(this);
            return;
        }

        switch (shaderType)
        {
            case SHADER.BAR_VIS: shaderName = "Custom/BarSurfaceShader";
                break;
            case SHADER.LINE_VIS: shaderName = "Custom/LineSurfaceShader";
                break;
            default:
                break;
        }
        m_mat = GetComponent<Renderer>().material;
        m_mat.shader = Shader.Find(shaderName);

        if (m_mat.shader.name != shaderName)
        {
            Debug.Log("Shader " + shaderName + " not found!");
            DestroyImmediate(this);
            return;
        }

        m_mat.SetVector("_Color", m_color);
        m_mat.SetTexture("_SoundImage", soundRef.m_soundTex);
        m_mat.SetFloat("_Emission", 1.0f);
    }
	
	void Update ()
    {
        m_mat.SetVector("_Color", m_color);
    }

	#region Private Functions

	#endregion
}
