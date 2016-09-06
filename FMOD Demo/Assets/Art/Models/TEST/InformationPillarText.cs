/*=================================================================
Project:		AIE FMOR
Developer:		Cameron Baron
Company:		FMOD
Date:			29/08/2016
==================================================================*/

using UnityEngine;
using System.Collections;

public class InformationPillarText : MonoBehaviour 
{
    // Public Vars
    public Guide m_guide;
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

    public void PlayAnim(float a_delayInSeconds = 0.0f)
    {
        if (a_delayInSeconds > 0.0f)
        {
            StartCoroutine(PlayAfterDelay(a_delayInSeconds));
        }
        else
        {
            anim.speed = 1;
        }
    }

    public void StopAnimAtEnd()
    {
        if (animSpeed == 0)
            anim.speed = 0;
    }

    public void SetAnimationSpeed(float a_speed)
    {
        animSpeed = a_speed;
    }

	#region Private Functions

    IEnumerator PlayAfterDelay(float a_delay)
    {
        yield return new WaitForSeconds(a_delay);

        anim.speed = 1;
    }

	#endregion
}
