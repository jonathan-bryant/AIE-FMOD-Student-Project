/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;


public class RoomEnterTrigger : MonoBehaviour 
{
    // Public Vars
    public RoomCompleted m_comp;
    // Private Vars
    SceneTrigger m_triggerScript;
    

	void Start () 
	{
        m_triggerScript = GetComponentInParent<SceneTrigger>();
	}
	
	void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            m_triggerScript.LoadRoom();
            m_comp.CompleteRoom();
        }
    }
}
