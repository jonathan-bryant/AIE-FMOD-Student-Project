/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			02/08/2016
==================================================================*/

using UnityEngine;


public class KillTrigger : HighlightableObject
{
    // Public Vars

    // Private Vars
    CreationTriggers m_triggerScript;

    void Start()
    {
        m_triggerScript = GetComponentInParent<CreationTriggers>();
    }

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			m_triggerScript.Kill();
		}
	}

    #region Private Functions

    #endregion
}
