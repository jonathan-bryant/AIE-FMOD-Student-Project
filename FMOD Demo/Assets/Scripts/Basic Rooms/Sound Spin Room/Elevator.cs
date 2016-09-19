/*===============================================================================================
|  Project:		FMOD Demo                                                                       |
|  Developer:	Matthew Zelenko                                                                 |
|  Company:		FMOD                                                                            |
|  Date:		20/09/2016                                                                      |
================================================================================================*/
using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    /////////////////////////fmod//////////////////////////////
    public FMODUnity.StudioEventEmitter m_event;

    GameObject m_player;
    public ElevatorDoor m_door;

    int m_currentFloor, m_selectedFloor;
    float m_selectedFloorHeight;
    bool m_isActive;
    bool m_isOpen;
    float m_elapsed;
    float m_originalZ;

	void Start () {
        m_currentFloor = 0;
        m_selectedFloor = 0;
        m_isActive = false;
        m_elapsed = 0.0f;
        m_isOpen = true;
        m_player = Camera.main.transform.parent.gameObject;
        m_originalZ = transform.position.z;
	}
	void Update () {
        m_elapsed += Time.deltaTime;
	    if(m_isActive)
        {
            if(m_isOpen)
            {
                if(m_elapsed >= 1.0f)
                {
                    m_isOpen = false;
                    m_elapsed = 0.0f;
                    /////////////////////////fmod//////////////////////////////
                    m_event.SetParameter("Intensity", 0);
                    m_event.Play();
                }
            }
            else
            {
                if(m_elapsed >= 2.0f + (Mathf.Abs(m_currentFloor - m_selectedFloor)))
                {
                    m_currentFloor = m_selectedFloor;
                    //Elevator
                    Vector3 pos = transform.position;
                    pos.y = m_selectedFloorHeight;
                    transform.position = pos;
                    //Player
                    pos = m_player.transform.position;
                    pos.y = m_selectedFloorHeight + 0.28f;
                    m_player.transform.position = pos;

                    m_isActive = false;
                    m_elapsed = 0.0f;
                }
                else
                {
                    Vector3 pos = transform.position;
                    pos.z = Mathf.Sin(Time.time * 10.0f) * 0.05f + m_originalZ;
                    transform.position = pos;
                }
            }
        }
        else
        {
            if (!m_isOpen)
            {
                if (m_elapsed >= 2.0f)
                {
                    m_door.OpenDoors();
                    m_isOpen = true;
                }
                else
                {
                    Vector3 pos = transform.position;
                    pos.z = Mathf.Sin(Time.time * 10.0f) * 0.05f + m_originalZ;
                    transform.position = pos;
                    /////////////////////////fmod//////////////////////////////
                    m_event.SetParameter("Intensity", 1);
                }
            }
        }
	}
    public void ChangeFloor(int a_floor, float a_height)
    {
        if (!m_door.m_doorsOpen || m_currentFloor == a_floor || m_isActive)
            return;

        m_selectedFloorHeight = a_height;
        m_isActive = true;
        m_elapsed = 0.0f;
        m_selectedFloor = a_floor;
        m_door.CloseDoors();
    }
}