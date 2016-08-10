using UnityEngine;
using System.Collections;

public class DoorTransceiver : MonoBehaviour
{

    public Door m_door;

    //---------------------------------Fmod-------------------------------
    //Call this to display it in Unity Inspector.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    //---------------------------------Fmod-------------------------------
    //Name of Event. Used in conjunction with EventInstance.
    //--------------------------------------------------------------------
    public string m_transceiverPath;
    //---------------------------------Fmod-------------------------------
    //EventInstance. Used to play or stop the sound, etc.
    //--------------------------------------------------------------------
    FMOD.Studio.EventInstance m_transceiverEvent;
    //---------------------------------Fmod-------------------------------
    //Parameter. Used to adjust EventInstances tracks. Such as: changing 
    //from wood to a carpet floor inside the same Event.
    //--------------------------------------------------------------------
    FMOD.Studio.ParameterInstance m_transceiverEnabledParameter;

    // Use this for initialization
    void Start()
    {
        m_transceiverEvent = FMODUnity.RuntimeManager.CreateInstance(m_transceiverPath);
        //---------------------------------Fmod-------------------------------
        //Create insance of footsteps event.
        //--------------------------------------------------------------------
        m_transceiverEvent = FMODUnity.RuntimeManager.CreateInstance(m_transceiverPath);
        //---------------------------------Fmod-------------------------------
        //Get a reference to the surface paramater and store it in
        //ParamaterInstance.
        //--------------------------------------------------------------------
        m_transceiverEvent.getParameter("Enabled", out m_transceiverEnabledParameter);
        //---------------------------------Fmod-------------------------------
        //Start the EventInstance.
        //--------------------------------------------------------------------
        m_transceiverEvent.start();
        //---------------------------------Fmod-------------------------------
        //Attach the instance to the gameobject when EventInstance.start() is 
        //called.
        //--------------------------------------------------------------------
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_transceiverEvent, transform, null);
        //---------------------------------Fmod-------------------------------
        //Set Enabled parameter to false. Door is closed.
        //--------------------------------------------------------------------
        m_transceiverEnabledParameter.setValue(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------Fmod-------------------------------
        //Set value of ParameterInstance to enabled if door is open.
        //--------------------------------------------------------------------
        m_transceiverEnabledParameter.setValue(m_door.DoorElapsed);

    }
}
