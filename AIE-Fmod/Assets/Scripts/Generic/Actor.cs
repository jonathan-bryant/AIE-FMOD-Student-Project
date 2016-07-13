using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    //Fmod: Call this to display it in Unity Inspector.
    [FMODUnity.EventRef]
    //Fmod: Name of Event. Used in conjunction with EventInstance.
    public string m_footstepSurfaceName;
    //Fmod: EventInstance. Used to play or stop the sound, etc.
    protected FMOD.Studio.EventInstance m_footstepSurfaceEvent;
    //Fmod: Parameter. Used to adjust EventInstances tracks. Such as: changing from wood to a carpet floor inside the same Event.
    protected FMOD.Studio.ParameterInstance m_footstepSurfaceParamter;
    
    public float m_movementSpeed;
    public float m_LookSensitivity;
    Rigidbody m_rb;
    Camera m_playerCamera;
    bool m_IsWalking = true;

    public void Start()
    {
        //FMOD: Create insance of footsteps event.
        m_footstepSurfaceEvent = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
        //FMOD: Get a reference to the surface paramater and store it in a ParamaterInstance.
        m_footstepSurfaceEvent.getParameter("Surface", out m_footstepSurfaceParamter);
        //Fmod: EventInstance.start() Safeguard. Used in Update().
        m_IsWalking = false;

        m_rb = GetComponent<Rigidbody>();
        m_playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0.0f, Input.GetAxis("Mouse X") * m_LookSensitivity, 0.0f);
        m_playerCamera.transform.rotation = m_playerCamera.transform.rotation * Quaternion.Euler(-Input.GetAxis("Mouse Y") * m_LookSensitivity, 0.0f, 0.0f);

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if (m_IsWalking)
            {
                //Fmod: When actor is idle, stop playing the sound.
                m_footstepSurfaceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                m_IsWalking = false;
            }
        }
        else
        {
            if (!m_IsWalking)
            {
                //Fmod: When actor is walking, start EventInstance. Calling EventInstance.start() will play the EventInstance from the beginning, so be sure to safeguard the call.
                m_footstepSurfaceEvent.start();
                m_IsWalking = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                m_rb.position += -transform.right * m_movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_rb.position += transform.right * m_movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                m_rb.position += transform.forward * m_movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_rb.position += -transform.forward * m_movementSpeed * Time.deltaTime;
            }
        }
        //Fmod: Have to set EventInstance position to current position every frame, otherwise the sound will come from where the actor started.
        m_footstepSurfaceEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform, m_rb));
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void FixedUpdate()
    {
        RaycastHit info;
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out info, 10.0f))
        {
            if (info.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                transform.position = new Vector3(transform.position.x, info.point.y + transform.localScale.y, transform.position.z);
            }
        }
    }
}
