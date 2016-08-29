/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;


public class InformationPillarText : MonoBehaviour 
{
    // Public Vars

    // Private Vars
    Animator anim;
    public string animName;
    public float timeOffset;

	void Start () 
	{
        anim = GetComponent<Animator>();

        anim.Play(animName, 0, timeOffset);
	}
	
	void Update () 
	{
	
	}

	#region Private Functions

	#endregion
}
