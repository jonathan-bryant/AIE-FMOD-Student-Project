using UnityEngine;
using System.Collections;

public class SurfacesPlayer : MonoBehaviour {

    [FMODUnity.EventRef]
    public string m_footstepSurfaceName;
    FMOD.Studio.EventInstance m_footstepSurfaceEvent;
    FMOD.Studio.ParameterInstance m_footstepSurfaceParamter;

    // Use this for initialization
    void Start()
    {
        m_footstepSurfaceEvent = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
        m_footstepSurfaceEvent.start();
        m_footstepSurfaceEvent.getParameter("Surface", out m_footstepSurfaceParamter);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider a_collider)
    {
        string tag = a_collider.gameObject.tag;
        if (tag == "Grass")
        {
            m_footstepSurfaceParamter.setValue(2.0f);
        }
        else if (tag == "Carpet")
        {
            m_footstepSurfaceParamter.setValue(1.0f);
        }
        else if (tag == "Wood")
        {
            m_footstepSurfaceParamter.setValue(0.9f);
        }
    }
    void OnTriggerExit(Collider a_collider)
    {
        string tag = a_collider.gameObject.tag;
        if (tag == "Grass")
        {
            m_footstepSurfaceParamter.setValue(0.0f);
        }
        else if (tag == "Tile")
        {
            m_footstepSurfaceParamter.setValue(0.0f);
        }
        else if (tag == "Wood")
        {
            m_footstepSurfaceParamter.setValue(0.0f);
        }
    }
}
