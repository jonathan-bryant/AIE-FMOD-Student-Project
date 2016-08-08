/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;


public class texstureTesting : MonoBehaviour 
{
    // Public Vars
    public MainSound soundRef;

	// Private Vars

	void Start ()
    {
        GetComponent<Renderer>().material.SetTexture("_SoundImage", soundRef.m_soundTex);
    }
	
	void Update ()
    {
    }

	#region Private Functions

	#endregion
}
