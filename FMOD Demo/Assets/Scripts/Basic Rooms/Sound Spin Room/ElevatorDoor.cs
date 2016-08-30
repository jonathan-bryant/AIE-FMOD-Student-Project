using UnityEngine;
using System.Collections;

public class ElevatorDoor : MonoBehaviour
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
            if (!m_doorsOpen)
            {
                Vector3 leftDoorPos = m_leftDoor.transform.localPosition;
                Vector3 rightDoorPos = m_rightDoor.transform.localPosition;

                leftDoorPos.z -= Time.deltaTime;
                rightDoorPos.z += Time.deltaTime;
                if (leftDoorPos.z < -1.4f)
                {
                    leftDoorPos.z = -1.4f;
                    rightDoorPos.z = 1.4f;
                    m_doorsOpen = true;
                    m_isActive = 0;
                }
                m_leftDoor.transform.localPosition = leftDoorPos;
                m_rightDoor.transform.localPosition = rightDoorPos;
            }
        }
        else if (m_isActive == -1)
        {
            //close
            if (m_doorsOpen)
            {
                Vector3 leftDoorPos = m_leftDoor.transform.localPosition;
                Vector3 rightDoorPos = m_rightDoor.transform.localPosition;

                leftDoorPos.z += Time.deltaTime;
                rightDoorPos.z -= Time.deltaTime;
                if (leftDoorPos.z > -0.5f)
                {
                    leftDoorPos.z = -0.5f;
                    rightDoorPos.z = 0.5f;
                    m_doorsOpen = false;
                    m_isActive = 0;
                }
                m_leftDoor.transform.localPosition = leftDoorPos;
                m_rightDoor.transform.localPosition = rightDoorPos;
            }
        }
    }

    public void OpenDoors()
    {
        m_isActive = 1;
    }
    public void CloseDoors()
    {
        m_isActive = -1;
    }
}