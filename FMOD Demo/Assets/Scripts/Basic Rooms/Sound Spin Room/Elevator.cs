/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Spin                                                      |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Sound Spinning is when a sound has a start, then a loop, than an|
|   end sound. So the sound will play the intro into a looping segment, then when it's told to  |
|   exit, it will play the exit segment. e.g. elevator, machine gun.                            |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    /*===============================================Fmod====================================================
    |   This line is a way to get an existing eventEmitter and control it from script                       |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_event;
    public FMODUnity.StudioEventEmitter m_elevatorMusic;

    GameObject m_player;
    public ElevatorDoor m_door;

    int m_currentFloor, m_selectedFloor;
    Vector3 m_selectedFloorPos;
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
                    /*===============================================Fmod====================================================
                    |   This is how you would go about setting parameters of an external eventEmitter.                      |
                    =======================================================================================================*/
                    m_event.SetParameter("End", 0);
                    m_event.Play();
                    m_elevatorMusic.SetParameter("End", 0);
                    m_elevatorMusic.Play();
                }
            }
            else
            {
                if(m_elapsed >= 2.0f + (Mathf.Abs(m_currentFloor - m_selectedFloor)))
                {
                    m_currentFloor = m_selectedFloor;
                    //Elevator
                    Vector3 move = m_selectedFloorPos - transform.position;
                    transform.position = m_selectedFloorPos;
                    //Player
                    m_player.transform.Translate(move);

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
                    /*===============================================Fmod====================================================
                    |   This is how you would go about setting parameters of an external eventEmitter.                      |
                    =======================================================================================================*/
                    m_event.SetParameter("End", 1);
                    m_elevatorMusic.SetParameter("End", 1);
                }
            }
        }
	}
    public void ChangeFloor(int a_floor, Vector3 a_pos)
    {
        if (!m_door.m_doorsOpen || m_currentFloor == a_floor || m_isActive)
            return;

        m_selectedFloorPos = a_pos;
        m_isActive = true;
        m_elapsed = 0.0f;
        m_selectedFloor = a_floor;
        m_door.CloseDoors();
    }
}