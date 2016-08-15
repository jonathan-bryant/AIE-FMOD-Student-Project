using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ActorControls : MonoBehaviour
{
    public float m_lookSensitivity;
    public float m_movementSpeed;
    public float m_selectDistance = 4.0f;
    public GameObject m_gun;

    CharacterController m_cc;
    Camera m_camera;

    float m_currentSpeed;
    Vector3 m_moveDirection;

    ActionObject m_actionObject;

    public bool m_disabledMovement;
    public bool m_disabledMouse;

    public float CurrentVelocity { get { return m_moveDirection.magnitude; } }

    void Start()
    {
        Application.runInBackground = true;
        m_cc = GetComponent<CharacterController>();
        m_camera = Camera.main;
        m_currentSpeed = m_movementSpeed;
        m_disabledMovement = true;
        m_disabledMouse = true;
        if (m_gun)
            m_gun.SetActive(false);
    }
    void Update()
    {
        DisableMovement();
        Action();
        Move();
        Look();
    }

    public void ActivateGun(bool a_value)
    {
        m_gun.SetActive(a_value);
    }
    void DisableMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                m_disabledMovement = false;
                m_disabledMouse = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                m_disabledMovement = true;
                m_disabledMouse = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    void Move()
    {
        m_moveDirection = Vector3.zero;
        if (!m_disabledMovement)
        {
            if (Input.GetKey(KeyCode.A))
            {
                m_moveDirection += -transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_moveDirection += transform.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                m_moveDirection += transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_moveDirection += -transform.forward;
            }
            m_moveDirection *= m_currentSpeed;
        }
        m_moveDirection.y -= 9.8f;
        m_cc.Move(m_moveDirection * Time.deltaTime);
    }
    void Look()
    {
        if (!m_disabledMouse && m_camera)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, Input.GetAxis("Mouse X") * m_lookSensitivity, 0.0f);
            m_camera.transform.rotation = m_camera.transform.rotation * Quaternion.Euler(-Input.GetAxis("Mouse Y") * m_lookSensitivity, 0.0f, 0.0f);
        }
    }
    void Action()
    {
        //Raycast
        RaycastHit ray;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ray, m_selectDistance))
        {
            //Disable last actionObject outline
            if (m_actionObject)
            {
                Material oldMat = m_actionObject.GetComponent<Renderer>().material;
                oldMat.SetInt("_OutlineEnabled", 0);
            }

            //Check if the new object is has actionObject, if so set the current m_actionObject to new object
            GameObject newObj = ray.collider.gameObject;
            ActionObject actionObject = newObj.GetComponent<ActionObject>();
            if (!actionObject)
            {
                actionObject = newObj.GetComponentInParent<ActionObject>();
                if (!actionObject)
                {
                    m_actionObject = null;
                    return;
                }
            }
            m_actionObject = actionObject;

            //Enable the objects outline
            Material mat = newObj.GetComponent<Renderer>().material;
            mat.SetInt("_OutlineEnabled", 1);

            //If the action key is pressed, call use on the actionObject
            if (Input.GetKeyDown(KeyCode.F))
            {
                m_actionObject.Use(gameObject);
            }
            return;
        }
        //If there is no raycast and there is an actionObject, disable it's outline, and call use but pass in false(Unuse basically)
        if (m_actionObject)
        {
            Material oldMat = m_actionObject.GetComponent<Renderer>().material;
            oldMat.SetInt("_OutlineEnabled", 0);
            m_actionObject = null;
        }
    }
}