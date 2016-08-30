/*=================================================================
Project:		AIE FMOR
Developer:		Cameron Baron
Company:		FMOD
Date:			29/08/2016
==================================================================*/

using UnityEngine;


public class InformationPillarText : MonoBehaviour 
{
    // Public Vars

    // Private Vars
    Animator anim;
    public string animName;
    public float timeOffset;
    public float animSpeed = 1;

	void Start () 
	{
        anim = GetComponent<Animator>();

        anim.Play(animName, 0, timeOffset);
	}
	
	void Update () 
	{
	
	}

    public void SetAnimationSpeed()
    {
        anim.speed = animSpeed; ;
    }

	#region Private Functions

	#endregion
}
