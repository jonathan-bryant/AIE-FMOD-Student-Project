using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    public float m_movementSpeed;
    public float m_currentSpeed;
    public float m_LookSensitivity;
    Camera m_playerCamera;
    bool m_IsWalking;
    public bool IsWalking { get { return this.m_IsWalking; } }
    CharacterController m_cc;
    Vector3 m_moveDirection;
    bool m_disableMovement;

    public void Start()
    {
        Application.runInBackground = true;
        m_IsWalking = false;

        m_playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        m_cc = GetComponent<CharacterController>();
        m_moveDirection = Vector3.zero;
        m_currentSpeed = m_movementSpeed;
    }

    public void Update()
    {
        if (!m_disableMovement)
        {
            m_moveDirection = Vector3.zero;
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, Input.GetAxis("Mouse X") * m_LookSensitivity, 0.0f);
            m_playerCamera.transform.rotation = m_playerCamera.transform.rotation * Quaternion.Euler(-Input.GetAxis("Mouse Y") * m_LookSensitivity, 0.0f, 0.0f);

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                m_IsWalking = false;
            }
            else
            {
                m_IsWalking = true;
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
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            m_disableMovement = !m_disableMovement;
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
        Use();
    }

    public void FixedUpdate()
    {
    }

    void OnTriggerStay(Collider a_col)
    {
    }

    void Use()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ray, 8.0f))
            {
                if (ray.collider.gameObject.tag == "Door")
                {
                    ray.collider.gameObject.GetComponent<Door>().Use();
                }
                if (ray.collider.gameObject.tag == "PanButton")
                {
                    ray.collider.gameObject.GetComponent<PanButton>().Use();
                }
            }
        }
    }
}
