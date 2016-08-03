using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour
{
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
    float m_windValue, m_rainValue;
    public float Wind { get { return m_windValue; } }
    public float Rain { get { return m_rainValue; } }
    int m_selectedElement;
    
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

        m_windValue = 0.0f;
        m_rainValue = 0.0f;
        m_selectedElement = 1;
    }
	
	void Update () {
        //Wind
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //m_selectedElement = 0;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            m_selectedElement = 1;
        }

        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            IncreaseElement();
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            DecreaseElement();
        }
    }
    void IncreaseElement()
    {
        if(m_selectedElement == 0)
        {
            m_windValue += Time.deltaTime;
            m_windValue = Mathf.Clamp(m_windValue, 0.0f, 1.0f);
            m_windParam.setValue(m_windValue);
        }
        else if(m_selectedElement == 1)
        {
            m_rainValue += Time.deltaTime;
            m_rainValue = Mathf.Clamp(m_rainValue, 0.0f, 1.0f);
            m_rainParam.setValue(m_rainValue);
        }
    }
    void DecreaseElement()
    {
        if (m_selectedElement == 0)
        {
            m_windValue -= Time.deltaTime;
            m_windValue = Mathf.Clamp(m_windValue, 0.0f, 1.0f);
            m_windParam.setValue(m_windValue);
        }
        else if (m_selectedElement == 1)
        {
            m_rainValue -= Time.deltaTime;
            m_rainValue = Mathf.Clamp(m_rainValue, 0.0f, 1.0f);
            m_rainParam.setValue(m_rainValue);
        }
    }
}
