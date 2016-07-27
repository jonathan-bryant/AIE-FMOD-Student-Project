using UnityEngine;
using System.Collections;

public class SG_MainSpeaker : MonoBehaviour {

    //---------------------------------Fmod-------------------------------
    //Call this to display it in Unity Inspector.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    //---------------------------------Fmod-------------------------------
    //Name of Event. Used in conjunction with EventInstance.
    //--------------------------------------------------------------------
    public string m_musicPath;
    //---------------------------------Fmod-------------------------------
    //EventInstance. Used to play or stop the sound, etc.
    //--------------------------------------------------------------------
    FMOD.Studio.EventInstance m_music;
    //---------------------------------Fmod-------------------------------
    //ParameterInstance. Used to reference a parameter stored in 
    //EventInstance. Example use case: changing 
    //from wood to carpet floor.
    //--------------------------------------------------------------------
    FMOD.Studio.ParameterInstance m_resultParam;
    FMOD.Studio.ParameterInstance m_activeParam;
    FMOD.Studio.ParameterInstance m_roundsParam;

    void Start () {
        //---------------------------------Fmod-------------------------------
        //Calling this function will create an EventInstance. The return value
        //is the created instance.
        //--------------------------------------------------------------------
        m_music = FMODUnity.RuntimeManager.CreateInstance(m_musicPath);

        //---------------------------------Fmod-------------------------------
        //Calling this function will return a reference to a parameter inside
        //EventInstance and store it in ParameterInstance.
        //--------------------------------------------------------------------
        m_music.getParameter("Result", out m_resultParam);
        m_music.getParameter("Active", out m_activeParam);
        m_music.getParameter("Round", out m_roundsParam);

        //---------------------------------Fmod-------------------------------
        //Calling this function will start the EventInstance.
        //--------------------------------------------------------------------
        m_music.start();
    }
	
	void Update () {

    }

    //---------------------------------Fmod-------------------------------
    //When these functions are called. It will change the parameter inside
    //EventInstance to the passed in value, changing the behaviour of the 
    //sound
    //--------------------------------------------------------------------
    public void SetGameResult(int a_result)
    {
        m_resultParam.setValue(a_result);
    }
    public void IsActive(bool a_active)
    {
        m_activeParam.setValue(a_active ? 1 : 0);
    }
    public void SetRound(int a_round)
    {
        m_roundsParam.setValue(a_round);
    }
}
