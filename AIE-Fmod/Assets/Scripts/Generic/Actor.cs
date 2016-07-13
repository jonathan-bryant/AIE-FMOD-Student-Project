using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string m_footstepSurfaceName;
    protected FMOD.Studio.EventInstance m_footstepSurfaceEvent;
    protected FMOD.Studio.ParameterInstance m_footstepSurfaceParamter;

    public float m_movementSpeed;
    public float m_LookSensitivity;
    Rigidbody m_rb;
    Camera m_playerCamera;
    bool m_IsWalking = true;

   

    public void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        //FMOD: Create insance of footsteps event
        m_footstepSurfaceEvent = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
        //FMOD: Get a reference to the surface paramater and store it in a ParamaterInstance
        m_footstepSurfaceEvent.getParameter("Surface", out m_footstepSurfaceParamter);
        m_IsWalking = false;
    
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
                m_footstepSurfaceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                m_IsWalking = false;
            }
        }
        else
        {
            if (!m_IsWalking)
            {
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
        m_footstepSurfaceEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform, m_rb));
    }

    public void FixedUpdate()
    {
        RaycastHit info;
        if(Physics.Raycast(transform.position, new Vector3(0,-1,0), out info, 10.0f))
        {
            if(info.collider.gameObject.tag == "Ground")
            {
                transform.position = new Vector3(transform.position.x, info.point.y + transform.localScale.y, transform.position.z);
            }
        }
    }
}
