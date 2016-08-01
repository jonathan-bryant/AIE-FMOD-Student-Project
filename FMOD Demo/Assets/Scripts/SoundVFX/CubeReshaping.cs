/*=================================================================
Project:		#AIE FMOD#
Developer:		#Cameron Baron#
Company:		#COMPANY#
Date:			#01/08/2016#
==================================================================*/

using UnityEngine;


public class CubeReshaping : MonoBehaviour 
{
    // Public Vars

    // Private Vars
    Material m_material;

    void Start ()
    {
        Shader.SetGlobalFloat("Height Adjustment", transform.position.x);

    }
	
	void Update () 
	{
        //m_material = GetComponent<Renderer>().material;
        //m_material.SetFloat("Height Adjustment", transform.position.x);

    }

	#region Private Functions

	#endregion
}
