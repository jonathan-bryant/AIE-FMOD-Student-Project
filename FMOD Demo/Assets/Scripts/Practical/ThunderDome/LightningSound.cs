/*===============================================================================================
|  Project:		FMOD Demo                                                                       |
|  Developer:	Matthew Zelenko                                                                 |
|  Company:		FMOD                                                                            |
|  Date:		20/09/2016                                                                      |
================================================================================================*/
using UnityEngine;
using System.Collections;

public class LightningSound : MonoBehaviour {
    
    //---------------------------------Fmod-------------------------------
    //Call this to display it in Unity Inspector.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    //---------------------------------Fmod-------------------------------
    //Name of Event. Used in conjunction with EventInstance.
    //--------------------------------------------------------------------
    public string m_thunderPath;

    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Play(int a_thunder)
    {
        //---------------------------------Fmod-------------------------------
        //EventInstance. Used to play or stop the sound, etc.
        //--------------------------------------------------------------------
        FMOD.Studio.EventInstance m_thunderEvent;
        //---------------------------------Fmod-------------------------------
        //Calling this function will create an EventInstance. The return value
        //is the created instance.
        //--------------------------------------------------------------------
        m_thunderEvent = FMODUnity.RuntimeManager.CreateInstance(m_thunderPath);
        //---------------------------------Fmod-------------------------------
        //Calling this function will return a reference to a parameter inside
        //EventInstance and store it in ParameterInstance.
        //--------------------------------------------------------------------
        m_thunderEvent.setParameterValue("Thunder", a_thunder);
        m_thunderEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, null));
        m_thunderEvent.start();
        m_thunderEvent.release();
    }
}
