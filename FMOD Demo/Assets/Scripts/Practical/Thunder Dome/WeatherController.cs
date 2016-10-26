/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Sets the parameters of the Event Instance from the individual   |
|   controllers.                                                                                |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour
{
    public WindSlider m_windController;
    public RainSlider m_rainController;
    public SunController m_sunController;
    /*===============================================FMOD====================================================
    |   Call this to display it in Unity Inspector.                                                         |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================FMOD====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_ambiencePath;
    /*===============================================FMOD====================================================
    |   EventInstance. Used to play or stop the sound, etc.                                                 |
    =======================================================================================================*/
    FMOD.Studio.EventInstance m_ambience;
    /*===============================================FMOD====================================================
    |   ParameterInstance. Used to reference a parameter stored in EventInstance. Example use case: changing|
    |   from wood to carpet floor.                                                                          |
    =======================================================================================================*/
    FMOD.Studio.ParameterInstance m_windParam;
    FMOD.Studio.ParameterInstance m_rainParam;
    FMOD.Studio.ParameterInstance m_sunParam;
    FMOD.Studio.ParameterInstance m_waterParam;
    public float Rain { get { return m_rainController.RainValue; } }

    void Start()
    {
        /*===============================================FMOD====================================================
        |   Calling this function will create an EventInstance. The return value is the created instance.       |
        =======================================================================================================*/
        m_ambience = FMODUnity.RuntimeManager.CreateInstance(m_ambiencePath);
        /*===============================================FMOD====================================================
        |   Calling this function will start the EventInstance.                                                 |
        =======================================================================================================*/
        m_ambience.start();
        m_ambience.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, null));
        /*===============================================FMOD====================================================
        |   Calling this function will return a reference to a parameter inside EventInstance and store it in   |
        |   ParameterInstance.                                                                                  |
        =======================================================================================================*/
        m_ambience.getParameter("Wind", out m_windParam);
        m_ambience.getParameter("Rain", out m_rainParam);
        m_ambience.getParameter("Water", out m_waterParam);
        m_ambience.getParameter("Time", out m_sunParam);
    }

    void Update()
    {
        /*===============================================FMOD====================================================
        |   This function is used to set the ParameterInstance value.                                           |
        =======================================================================================================*/
        m_windParam.setValue(m_windController.WindValue);
        m_rainParam.setValue(m_rainController.RainValue);
        m_waterParam.setValue(m_rainController.WaterValue);
        m_sunParam.setValue(m_sunController.SunValue);
    }

    void OnDestroy()
    {
        /*===============================================Fmod====================================================
        |   This function stops the event. It takes in a parameter of type FMOD.Studio.STOP_MODE. Used for      |
        |   stopping events gradually rather than instantly.                                                    |
        =======================================================================================================*/
        m_ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
