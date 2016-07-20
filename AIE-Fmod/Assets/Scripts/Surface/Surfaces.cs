using UnityEngine;
using System.Collections;

public class Surfaces : MonoBehaviour
{
    Actor m_actor;
    bool m_oldIsWalking;
    Collider m_lastCollider;

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
        m_oldIsWalking = false;
        m_actor = GetComponent<Actor>();

        //---------------------------------Fmod-------------------------------
        //Create insance of footsteps event.
        //--------------------------------------------------------------------
        m_footstepSurfaceEvent = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
        //---------------------------------Fmod-------------------------------
        //Get a reference to the surface paramater and store it in
        //ParamaterInstance.
        //--------------------------------------------------------------------
        m_footstepSurfaceEvent.getParameter("Surface", out m_footstepSurfaceParameter);
        //---------------------------------Fmod-------------------------------
        //Set pitch of EventInstance to movement speed of actor.
        //--------------------------------------------------------------------
        m_footstepSurfaceEvent.setPitch(m_actor.m_movementSpeed / 8.0f);
    }

    void Update()
    {
        //---------------------------------Fmod--------------------wwwd-----------
        //IsWalking is a safeguard so the EventInstance.start() function is
        //not called every update. Calling EventInstance.start() without
        //calling EventInstance.stop() will restart the instance.
        //--------------------------------------------------------------------
        if ((!m_actor.IsWalking && m_oldIsWalking))
        {
            //---------------------------------Fmod-------------------------------
            //When actor is idle, call EventInstance.stop().
            //--------------------------------------------------------------------
            m_footstepSurfaceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else if ((m_actor.IsWalking && !m_oldIsWalking))
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
        }
        m_oldIsWalking = m_actor.IsWalking;
    }

    void FixedUpdate()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit a_hit)
    {
        //---------------------------------Fmod-------------------------------
        //Depending on the type of ground that is triggered, call ParameterInstance.setValue() to change the transition of the footstep.
        //--------------------------------------------------------------------
        string tag = a_hit.gameObject.tag;
        if (m_lastCollider == a_hit.collider)
            return;
       
        if (tag == "Grass")
        {
            m_footstepSurfaceParameter.setValue(2.0f);
        }
        else if (tag == "Carpet")
        {
            m_footstepSurfaceParameter.setValue(1.0f);
        }
        else if (tag == "Wood")
        {
            m_footstepSurfaceParameter.setValue(3.0f);
        }
        else
        {
            m_footstepSurfaceParameter.setValue(0.0f);
        }


        //---------------------------------Fmod-------------------------------
        //If actor is going up stairs slow the movement speed and call 
        //EventInstance.setPitch() appropriately.
        //--------------------------------------------------------------------
        bool currentStep = a_hit.collider.gameObject.name.Contains("Step");
        bool lastStep = false;
        if (m_lastCollider)
            lastStep = m_lastCollider.gameObject.name.Contains("Step");
        if (currentStep && !lastStep)
        {
            m_actor.m_currentSpeed /= 2.5f;
            //---------------------------------Fmod-------------------------------
            //Set pitch of EventInstance to movement speed of actor.
            //--------------------------------------------------------------------
            m_footstepSurfaceEvent.setPitch(m_actor.m_currentSpeed / 8.0f);
        }
        else if (!currentStep && lastStep)
        {
            m_actor.m_currentSpeed = m_actor.m_movementSpeed;
            //---------------------------------Fmod-------------------------------
            //Set pitch of EventInstance to movement speed of actor.
            //--------------------------------------------------------------------
            m_footstepSurfaceEvent.setPitch(m_actor.m_currentSpeed / 8.0f);
        }
        m_lastCollider = a_hit.collider;
        if (m_actor.IsWalking)
        {
            //---------------------------------Fmod-------------------------------
            //When surface changes, call EventInstance.start() to restart.
            //--------------------------------------------------------------------
            m_footstepSurfaceEvent.start();
            //---------------------------------Fmod-------------------------------
            //A gameobject need to be attached to the instance, so the sound can 
            //follow the gameobject. Everytime the EventInstance.start() function 
            //is called, the gameobject needs to be reattached.
            //--------------------------------------------------------------------
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_footstepSurfaceEvent, transform, null);
        }
    }
}
