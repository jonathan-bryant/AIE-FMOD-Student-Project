/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Controls the elevator.                                          |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class O_Elevator : MonoBehaviour
{
    //---------------------------------Fmod-------------------------------
    [FMODUnity.EventRef]    public string m_event = "event:/Elevator";
    FMOD.Studio.EventInstance m_eventInstance;
    string m_eventParamString = "End of journey";
    FMOD.Studio.ParameterInstance m_eventParam;
    public float m_eventVolume = 0.1f;
    //--------------------------------------------------------------------

    public GameObject m_outerDoorHolder;
    public O_ElevatorDoor m_door;
    public float m_speed;

    ActorControls m_actor;
    int m_isActive; //0 off, 1 close, 2 lift, 3 open
    int m_currentFloor;
    int m_selectedFloor;
    float m_selectedFloorY;
    int m_direction;

    float m_elapsed;
    public float m_startFloorY;

    void Start()
    {
        //---------------------------------Fmod-------------------------------
        //  Create an instance, get the parameter to control the "outro" and
        //  adjust the volume.
        //--------------------------------------------------------------------
        m_eventInstance = FMODUnity.RuntimeManager.CreateInstance(m_event);
        m_eventInstance.getParameter(m_eventParamString, out m_eventParam);
        m_eventInstance.setVolume(m_eventVolume);

        m_actor = Camera.main.transform.parent.gameObject.GetComponent<ActorControls>();
        m_isActive = 0;
        m_currentFloor = -1;
        m_selectedFloor = -1;
        m_selectedFloorY = m_startFloorY;
        m_actor.m_disabledMovement = true;
    }
    void Update()
    {
        if (m_selectedFloor == -1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_selectedFloor = 0;
                m_isActive = 1;
                m_actor.m_disabledMovement = false;

                StartSoundEvent();
            }
        }

        if (m_isActive == 1)
        {
            Vector3 playerPos = m_actor.transform.position;
            //Unity's Cylinder Collider
            Vector3 playerXZ = playerPos; playerXZ.y = 0.0f;
            Vector3 elevatorXZ = transform.position; elevatorXZ.y = 0.0f;
            if ((playerXZ - elevatorXZ).magnitude < 0.8f && playerPos.y - 0.7f >= transform.position.y - 1.459 && playerPos.y - 0.7f <= transform.position.y + 1.459)
            {
                CenterActor();
                m_actor.m_disabledMovement = true;
            }
            if (!m_door.IsDoorOpen)
            {
                StartSoundEvent();
                m_isActive = 2;
                if (m_selectedFloor < m_currentFloor)
                    m_direction = -1;
                else
                    m_direction = 1;
            }
        }
        else if (m_isActive == 2)
        {
            CenterActor();
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
                else if (pos.y <= m_selectedFloorY + 2.0f)
                {
                    m_eventParam.setValue(1.0f);
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
                else if (pos.y >= m_selectedFloorY - 2.0f)
                {
                    m_eventParam.setValue(1.0f);
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
            if ((playerXZ - elevatorXZ).magnitude < 0.8f && playerPos.y - 0.7f >= transform.position.y - 1.459 && playerPos.y - 0.7f <= transform.position.y + 1.459)
            {
                playerPos.y = pos.y - 1.0f + 0.7f;
                m_actor.transform.position = playerPos;
                m_actor.m_disabledMovement = true;
            }

        }
        else if (m_isActive == 3)
        {
            m_actor.m_disabledMovement = false;
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

        Vector3 playerPos = m_actor.transform.position;
        Vector3 playerXZ = playerPos; playerXZ.y = 0.0f;
        Vector3 elevatorXZ = transform.position; elevatorXZ.y = 0.0f;
        if ((playerXZ - elevatorXZ).magnitude < 0.8f && playerPos.y - 0.7f >= transform.position.y - 1.459 && playerPos.y - 0.7f <= transform.position.y + 1.459)
        {
            m_actor.m_disabledMovement = true;
        }
    }
    void CenterActor()
    {
        Vector3 playerPos = m_actor.transform.position;
        Vector3 playerXZ = playerPos; playerXZ.y = 0.0f;
        Vector3 elevatorXZ = transform.position; elevatorXZ.y = 0.0f;
        if ((playerXZ - elevatorXZ).magnitude < 0.8f && playerPos.y - 0.7f >= transform.position.y - 1.459 && playerPos.y - 0.7f <= transform.position.y + 1.459)
        {
            Vector3 elevatorMiddleDirection = transform.position - m_actor.transform.position;
            elevatorMiddleDirection.y = 0;
            if (elevatorMiddleDirection.magnitude > 1.0f)
            {
                elevatorMiddleDirection.Normalize();
            }
            if (elevatorMiddleDirection.magnitude != 0.0f)
                m_actor.GetComponent<CharacterController>().Move(elevatorMiddleDirection * 0.05f);
        }
    }

    void StartSoundEvent()
    {
        m_eventParam.setValue(0.0f);
        m_eventInstance.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_eventInstance, transform, null);
    }
}