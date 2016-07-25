using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ActorControls : MonoBehaviour
{

    public float m_movementSpeed;
    float m_currentSpeed;
    public float m_lookSensitivity;
    Camera m_playerCamera;
    CharacterController m_cc;
    bool m_disableMovement;
    Vector3 m_moveDirection;
    bool m_riding;
    GameObject m_actionObject;
    public GameObject m_gun;

    void Start()
    {
        Application.runInBackground = true;

        m_playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        m_cc = GetComponent<CharacterController>();
        m_currentSpeed = m_movementSpeed;
        m_gun.SetActive(false);
    }

    void Update()
    {
        DisableMovement();
        Action();
        Move();
        Look();
        Ride();
    }
    void DisableMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                m_disableMovement = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                m_disableMovement = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    void Move()
    {
        if (m_riding)
            return;
        m_moveDirection = Vector3.zero;
        if (!m_disableMovement)
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
        if (!m_disableMovement && m_playerCamera)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, Input.GetAxis("Mouse X") * m_lookSensitivity, 0.0f);
            m_playerCamera.transform.rotation = m_playerCamera.transform.rotation * Quaternion.Euler(-Input.GetAxis("Mouse Y") * m_lookSensitivity, 0.0f, 0.0f);
        }
    }
    void Ride()
    {
        if(m_actionObject && m_actionObject.name.Contains("Cart"))
        {
            Cart cart = m_actionObject.GetComponentInParent<Cart>();
            transform.position = cart.m_seat.transform.position;
        }
    }
    void Action()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 4.0f, Color.green, 4.0f);
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ray, 4.0f))
            {
                if (ray.collider.gameObject.name.Contains("Cart"))
                {
                    if (m_actionObject == ray.collider.gameObject)
                    {
                        m_actionObject = null;
                        m_riding = false;
                        m_gun.SetActive(false);
                    }
                    else
                    {
                        m_actionObject = ray.collider.gameObject;
                        m_riding = true;
                        m_gun.SetActive(true);
                    }
                    return;
                }                    
            }
            m_actionObject = null;
            m_riding = false;
            m_gun.SetActive(false);
        }
    }
}