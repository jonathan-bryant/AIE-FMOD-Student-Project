/*=================================================================
Project:		#PROJECTNAME#
Developer:		Cameron Baron
Company:		FMOD
Date:			25/07/2016
==================================================================*/

using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarFmodSound : MonoBehaviour 
{
    // Public Vars
    [FMODUnity.EventRef]
        public string m_eventPath = "";

    [FMODUnity.EventRef]
    public string m_wheelSkid = "";

    // Engine sound vars
    private FMOD.Studio.EventInstance m_engineInstance;
    private FMOD.Studio.EventDescription m_engineDescription;
    private FMOD.Studio.ParameterInstance m_rpmParam;
    private FMOD.Studio.PARAMETER_DESCRIPTION m_rpmDesc;
    private FMOD.Studio.ParameterInstance m_loadParam;
    private FMOD.Studio.PARAMETER_DESCRIPTION m_loadDesc;    
    private float m_engineRPM;
    private float m_engineLoad;

    // Wheel sound vars
    private FMOD.Studio.EventInstance m_wheelSkidInstance;
    private FMOD.Studio.EventDescription m_wheelSkidDesccription;
    private FMOD.Studio.ParameterInstance m_wheelMaterialContact;
    private float m_wheelContact;

    private CarController m_carController;

    private FMOD.RESULT result;

	void Start () 
	{
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<AudioListener>().enabled = false;
        if (camera.GetComponent<FMODUnity.StudioListener>() == null)
        {
            //camera.AddComponent<FMODUnity.StudioListener>();
        }
            gameObject.AddComponent<FMODUnity.StudioListener>();
        GetComponent<CarAudio>().enabled = false;
        m_carController = GetComponent<CarController>();
        // If this event has not been set, destroy this script
	    if (m_eventPath == "")
        {
            Debug.LogError("No event assigned to " + gameObject.name);
            Destroy(this);
            return;
        }

        // Create sound instance
        m_engineInstance = FMODUnity.RuntimeManager.CreateInstance(m_eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_engineInstance, gameObject.transform, null);
        // Assign parameters
        result = m_engineInstance.getParameter("RPM", out m_rpmParam);
        result = m_rpmParam.getDescription(out m_rpmDesc);
        result = m_engineInstance.getParameter("Load", out m_loadParam);
        result = m_loadParam.getDescription(out m_loadDesc);

        m_engineInstance.start();
    }
	
	void Update () 
	{
        float RPM = m_carController.Revs; // Revs from Unity's car controller are between 0 - 1.
        m_engineRPM = m_rpmDesc.maximum * RPM;
        m_rpmParam.setValue(m_engineRPM);

        int gear = m_carController.m_GearNum;
        m_engineLoad = gear * 1 / (m_loadDesc.maximum - m_loadDesc.minimum) - 1.0f;
        m_loadParam.setValue(m_engineLoad);
	}

    void PlayWheelSkid()
    {
        m_wheelSkidInstance = FMODUnity.RuntimeManager.CreateInstance(m_wheelSkid);
        m_wheelSkidInstance.start();
    }

    void StopWheelSkid()
    {

    }

	#region Private Functions
    

	#endregion
}
