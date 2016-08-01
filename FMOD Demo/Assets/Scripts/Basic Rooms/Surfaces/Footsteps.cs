using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
    ActorControls m_actor;
    Collider m_lastCollider;
    bool m_isWalking;

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
    //---------------------------------Fmod-------------------------------
    //Parameter. Used to adjust EventInstances tracks. Such as: changing 
    //from wood to a carpet floor inside the same Event.
    //--------------------------------------------------------------------
    FMOD.Studio.ParameterInstance m_footstepSurfaceParameter;

    void Start()
    {
        m_isWalking = false;
        m_actor = GetComponent<ActorControls>();

        //---------------------------------Fmod-------------------------------
        //Create insance of footsteps event.
        //--------------------------------------------------------------------
        m_footstepSurfaceEvent = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
        //---------------------------------Fmod-------------------------------
        //Get a reference to the surface paramater and store it in
        //ParamaterInstance.
        //--------------------------------------------------------------------
        m_footstepSurfaceEvent.getParameter("Surface", out m_footstepSurfaceParameter);
    }

    void Update()
    {
        //---------------------------------Fmod--------------------wwwd-----------
        //IsWalking is a safeguard so the EventInstance.start() function is
        //not called every update. Calling EventInstance.start() without
        //calling EventInstance.stop() will restart the instance.
        //--------------------------------------------------------------------
        if (m_actor.CurrentVelocity <= 9.8f && m_isWalking)
        {
            //---------------------------------Fmod-------------------------------
            //When actor is idle, call EventInstance.stop().
            //--------------------------------------------------------------------
            m_footstepSurfaceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            m_isWalking = false;
        }
        else if (m_actor.CurrentVelocity > 9.8f && !m_isWalking)
        {
            //---------------------------------Fmod-------------------------------
            //When actor is walking, call EventInstance.start().
            //--------------------------------------------------------------------
            m_footstepSurfaceEvent.start();
            //---------------------------------Fmod-------------------------------
            //A gameobject need to be attached to the instance, so the sound can 
            //follow the gameobject. Everytime the EventInstance.start() function 
            //is called, the gameobject needs to be reattached.
            //--------------------------------------------------------------------
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_footstepSurfaceEvent, transform, null);

            m_isWalking = true;
        }
    }

    void FixedUpdate()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit a_hit)
    {
        //---------------------------------Fmod-------------------------------
        //Depending on the type of ground that is triggered, call ParameterInstance.setValue() to change the transition of the footstep.
        //--------------------------------------------------------------------
        string name = a_hit.gameObject.name;
        if (m_lastCollider == a_hit.collider)
            return;

        if (name.Contains("Grass"))
        {
            m_footstepSurfaceParameter.setValue(2.0f);
        }
        else if (name.Contains("Carpet"))
        {
            m_footstepSurfaceParameter.setValue(1.0f);
        }
        else if (name.Contains("Wood"))
        {
            m_footstepSurfaceParameter.setValue(3.0f);
        }
        else
        {
            m_footstepSurfaceParameter.setValue(0.0f);
        }

        m_lastCollider = a_hit.collider;
    }
}
