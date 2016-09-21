/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls the elevator.                                          |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class O_Elevator : MonoBehaviour
{
    public GameObject m_outerDoorHolder;
    public O_ElevatorDoor m_door;
    public float m_speed;

    Transform m_actor;
    int m_isActive; //0 off, 1 close, 2 lift, 3 open
    int m_currentFloor;
    int m_selectedFloor;
    float m_selectedFloorY;
    int m_direction;

    float m_elapsed;
    public float m_startFloorY;

    void Start()
    {
        m_actor = Camera.main.transform.parent;
        m_isActive = 1;
        m_currentFloor = -1;
        m_selectedFloor = 0;
        m_selectedFloorY = m_startFloorY;
    }
    void Update()
    {
        if (m_isActive == 1)
        {
            if (!m_door.IsDoorOpen)
            {
                m_isActive = 2;
                if (m_selectedFloor < m_currentFloor)
                    m_direction = -1;
                else
                    m_direction = 1;
            }
        }
        else if (m_isActive == 2)
        {
            transform.Translate(0.0f, m_direction * m_speed * Time.deltaTime, 0.0f);
            Vector3 pos = transform.position;
            bool finished = false;
            if (m_direction == -1)
            {
                if (pos.y <= m_selectedFloorY)
                {
                    pos.y = m_selectedFloorY;
                    transform.position = pos;
                    finished = true;
                }
            }
            else
            {
                if (pos.y >= m_selectedFloorY)
                {
                    pos.y = m_selectedFloorY;
                    transform.position = pos;
                    finished = true;
                }
            }
            if (finished)
            {
                m_currentFloor = m_selectedFloor;
                m_isActive = 3;
                m_door.OpenDoor();
                m_outerDoorHolder.transform.GetChild(m_currentFloor).GetComponent<O_ElevatorDoor>().OpenDoor();
            }

            Vector3 playerPos = m_actor.transform.position;
            //Unity's Cylinder Collider
            Vector3 playerXZ = playerPos; playerXZ.y = 0.0f;
            Vector3 elevatorXZ = transform.position; elevatorXZ.y = 0.0f;
            if (m_currentFloor == -1 || (playerXZ - elevatorXZ).magnitude < 0.8f && playerPos.y - 0.7f >= transform.position.y - 1.459 && playerPos.y - 0.7f <= transform.position.y + 1.459)
            {
                playerPos.y = pos.y - 1.0f + 0.7f;
                m_actor.transform.position = playerPos;
            }
        }
        else if (m_isActive == 3)
        {
            if (m_door.IsDoorOpen)
            {
                m_isActive = 0;
            }
        }
    }

    public void ChangeFloor(int a_floor, float a_floorY)
    {
        if (m_isActive != 0 || m_currentFloor == a_floor)
            return;

        m_isActive = 1;
        m_selectedFloor = a_floor;
        m_selectedFloorY = a_floorY;
        m_door.CloseDoor();
        m_outerDoorHolder.transform.GetChild(m_currentFloor).GetComponent<O_ElevatorDoor>().CloseDoor();
    }
}