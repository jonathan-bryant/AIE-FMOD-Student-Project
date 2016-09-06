using UnityEngine;
using System.Collections;

public class O_ElevatorDoor : MonoBehaviour
{
    public GameObject m_leftDoor, m_rightDoor;
    int m_isActive;
    bool m_doorsOpen;

    void Start()
    {
        m_isActive = 0;
        m_doorsOpen = true;
    }
    void Update()
    {
        if (m_isActive == 1)
        {
            //open
            Vector3 leftDoorRot = m_leftDoor.transform.localEulerAngles;
            Vector3 rightDoorRot = m_rightDoor.transform.localEulerAngles;

            leftDoorRot.y -= 90.0f * Time.deltaTime;
            rightDoorRot.y += 90.0f * Time.deltaTime;
            if (leftDoorRot.z < 0)
            {
                leftDoorRot.y = 0.0f;
                rightDoorRot.y = 0.0f;
                m_doorsOpen = false;
                m_isActive = 0;
            }
            m_leftDoor.transform.localEulerAngles = leftDoorRot;
            m_rightDoor.transform.localEulerAngles = rightDoorRot;
        }
        else if (m_isActive == -1)
        {
            //close
            Vector3 leftDoorRot = m_leftDoor.transform.localEulerAngles;
            Vector3 rightDoorRot = m_rightDoor.transform.localEulerAngles;

            leftDoorRot.y += 90.0f * Time.deltaTime;
            rightDoorRot.y -= 90.0f * Time.deltaTime;
            if (leftDoorRot.z > 90.0f)
            {
                leftDoorRot.y = 90.0f;
                rightDoorRot.y = -90.0f;
                m_doorsOpen = false;
                m_isActive = 0;
            }
            m_leftDoor.transform.localEulerAngles = leftDoorRot;
            m_rightDoor.transform.localEulerAngles = rightDoorRot;
        }
    }

    public void OpenDoors()
    {
        if (m_doorsOpen)
            return;
        m_isActive = 1;
    }
    public void CloseDoors()
    {
        if (!m_doorsOpen)
            return;
        m_isActive = -1;
    }
}