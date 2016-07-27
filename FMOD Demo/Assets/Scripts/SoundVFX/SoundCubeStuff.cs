/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;


public class SoundCubeStuff : MonoBehaviour 
{
    // Public Vars

    // Private Vars
    Material material;

	void Start () 
	{
        Vector3 uvPos = (transform.position - transform.parent.parent.position);
        material = GetComponent<Renderer>().material;
        uvPos.x = 1.0f / (Mathf.Abs(uvPos.x)) * -1.0f;
        material.SetFloat("UVx", uvPos.x);
        uvPos.z = 1.0f / (Mathf.Abs(uvPos.z)) * -1.0f;
        material.SetFloat("UVy", uvPos.z);
	}
	
	void Update () 
	{
	
	}

	#region Private Functions

	#endregion
}
