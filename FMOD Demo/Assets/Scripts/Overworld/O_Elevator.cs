using UnityEngine;
using System.Collections;

public class O_Elevator : MonoBehaviour
{
    public O_ElevatorDoor m_leftDoor, m_rightDoor;
    public int m_currentFloor;
    int m_selectedFloor;
    int m_isActive; //0 off, 1 close, 2 lift, 3 open
    int m_doorsState;
    float m_selectedFloorY;
    public float m_speed;
    float m_elapsed;
    Transform m_actor;

    void Start()
    {
        m_actor = Camera.main.transform.parent;
        m_isActive = 0;
        m_selectedFloor = m_currentFloor;
        m_currentFloor = 0;
        m_selectedFloorY = -3.5f;
    }
    void Update()
    {
        if (m_isActive == 0)
            return;
        m_elapsed += Time.deltaTime;
        if (m_isActive == 1)
        {
            if (m_elapsed >= 1.0f)
            {
                m_isActive = 2;
                m_elapsed = 0.0f;
            }
        }
        else if (m_isActive == 2)
        {
            if (m_selectedFloor > m_currentFloor)
            {
                transform.Translate(0.0f, m_speed * Time.deltaTime, 0.0f);
                if((m_actor.position - transform.position).magnitude < 1.5f)
                    m_actor.Translate(0.0f, m_speed * Time.deltaTime, 0.0f);
                if (transform.position.y > m_selectedFloorY)
                {
                    Vector3 pos = transform.position;
                    pos.y = m_selectedFloorY;
                    transform.position = pos;
                    m_isActive = 3;
                }
            }
            else
            {
                transform.Translate(0.0f, -m_speed * Time.deltaTime, 0.0f);
                if ((m_actor.position - transform.position).magnitude < 1.5f)
                    m_actor.Translate(0.0f, -m_speed * Time.deltaTime, 0.0f);
                if (transform.position.y < m_selectedFloorY)
                {
                    Vector3 pos = transform.position;
                    pos.y = m_selectedFloorY;
                    transform.position = pos;
                    m_isActive = 3;
                }
            }
        }
        else if (m_isActive == 3)
        {
            if (m_elapsed >= 1.0f)
            {
                m_currentFloor = m_selectedFloor;
                m_isActive = 0;
                m_elapsed = 0.0f;
                if (m_leftDoor)
                {
                    m_leftDoor.OpenDoors();
                    m_rightDoor.OpenDoors();
                }
            }
        }
    }

    public void ChangeFloor(int a_floor)
    {
        if (m_isActive != 0 || m_currentFloor == a_floor)
            return;

        m_isActive = 1;
        m_selectedFloor = a_floor;
        m_selectedFloorY = -3.5f + 5.0f * a_floor;
        if (m_leftDoor)
        {
            m_leftDoor.CloseDoors();
            m_rightDoor.CloseDoors();
        }
    }
}