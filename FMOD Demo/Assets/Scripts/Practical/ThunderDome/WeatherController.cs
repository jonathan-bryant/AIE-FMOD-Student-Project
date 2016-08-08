using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour
{
    public WindController m_windController;
    public RainController m_rainController;
    //---------------------------------Fmod-------------------------------
    //Call this to display it in Unity Inspector.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    //---------------------------------Fmod-------------------------------
    //Name of Event. Used in conjunction with EventInstance.
    //--------------------------------------------------------------------
    public string m_ambiencePath;
    //---------------------------------Fmod-------------------------------
    //EventInstance. Used to play or stop the sound, etc.
    //--------------------------------------------------------------------
    FMOD.Studio.EventInstance m_ambience;
    //---------------------------------Fmod-------------------------------
    //ParameterInstance. Used to reference a parameter stored in 
    //EventInstance. Example use case: changing 
    //from wood to carpet floor.
    //--------------------------------------------------------------------
    FMOD.Studio.ParameterInstance m_windParam;
    FMOD.Studio.ParameterInstance m_rainParam;
    public float Wind { get { return m_windController.m_windValue; } }
    public float Rain { get { return m_rainController.m_rainValue; } }
    
    void Start () {
        //---------------------------------Fmod-------------------------------
        //Calling this function will create an EventInstance. The return value
        //is the created instance.
        //--------------------------------------------------------------------
        m_ambience = FMODUnity.RuntimeManager.CreateInstance(m_ambiencePath);

        //---------------------------------Fmod-------------------------------
        //Calling this function will return a reference to a parameter inside
        //EventInstance and store it in ParameterInstance.
        //--------------------------------------------------------------------
        m_ambience.getParameter("Wind", out m_windParam);
        m_ambience.getParameter("Rain", out m_rainParam);

        //---------------------------------Fmod-------------------------------
        //Calling this function will start the EventInstance.
        //--------------------------------------------------------------------
        m_ambience.start();
    }
	
	void Update () {
        m_windParam.setValue(m_windController.m_windValue);
        m_rainParam.setValue(m_rainController.m_rainValue);
    }
}
