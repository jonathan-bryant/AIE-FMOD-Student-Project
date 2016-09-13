/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;


public class ButtonPressScript : ActionObject 
{
    // Public Vars
    public GameObject m_grenade;
    public Transform m_grenadeSpawn;

    [FMODUnity.EventRef]
    public string m_musicEventString;

    // Private Vars
    FMOD.Studio.EventInstance m_musicEventInstance;


	void Start () 
	{
        m_musicEventInstance = FMODUnity.RuntimeManager.CreateInstance(m_musicEventString);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_musicEventInstance, transform, null);
        m_musicEventInstance.start();
	}

    void OnDestroy()
    {
        m_musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        m_musicEventInstance.release();
    }
	
	void Update () 
	{
	
	}

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        SpawnObject();
    }

	#region Private Functions
    
    void SpawnObject()
    {
        // if timer < 0
        GameObject nade = Instantiate(m_grenade, m_grenadeSpawn.position, Quaternion.identity) as GameObject;
    }

	#endregion
}
