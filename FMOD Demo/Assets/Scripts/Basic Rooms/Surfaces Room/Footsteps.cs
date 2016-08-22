using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
    ActorControls m_actor;
    public float m_fps;         //Footsteps per second
    float m_fpsElapsed;
    float m_currentParamValue;  //1.0f == carpet, 2.0f == grass, 3.0f == wood

    //---------------------------------Fmod-------------------------------
    //Call this to display it in Unity Inspector.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    //---------------------------------Fmod-------------------------------
    //Name of Event. Used in conjunction with EventInstance.
    //--------------------------------------------------------------------
    public string m_footstepSurfaceName;
    //---------------------------------Fmod-------------------------------
    //EventInstance. Used to play or stop the sound, etc.
    //--------------------------------------------------------------------
    FMOD.Studio.EventInstance m_footstepSurfaceEvent;


    void Start()
    {
        m_actor = GetComponent<ActorControls>();
        m_fpsElapsed = 0.0f;
        m_currentParamValue = 0.0f;
    }

    void Update()
    {
        m_fpsElapsed += Time.deltaTime;
        Vector3 velocity = m_actor.CurrentVelocity;
        velocity.y = 0.0f;
        if (velocity.magnitude > 0.0f && m_actor.IsGrounded && m_fpsElapsed >= m_fps)
        {
            if (m_currentParamValue > 0.99f)
            {
                FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
                instance.setParameterValue("Surface", m_currentParamValue);
                instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position - new Vector3(0,transform.localScale.y,0)));
                instance.start();
                instance.release();
            }
            m_fpsElapsed = 0.0f;
        }
    }

    void FixedUpdate()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit a_hit)
    {
        string name = a_hit.gameObject.name;
        string tag = a_hit.gameObject.tag;

        if (name.Contains("Grass") || tag == "Grass")
        {
            m_currentParamValue = 2.0f;
        }
        else if (name.Contains("Carpet") || tag == "Carpet")
        {
            m_currentParamValue = 1.0f;
        }
        else if (name.Contains("Wood") || tag == "Wood")
        {
            m_currentParamValue = 3.0f;
        }
        else
        {
            m_currentParamValue = 0.0f;
        }
    }
}