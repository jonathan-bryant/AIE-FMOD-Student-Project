using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    bool m_doorOpen;
    public bool DoorOpen { get { return m_doorOpen; } }

    void Start()
    {
        

    }

    void Update()
    {

    }

    public void Use()
    {
        if (!m_doorOpen)
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 90.0f);
            transform.position += new Vector3(2.0f, 0.0f, 2.0f);
            m_doorOpen = true;
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -90.0f);
            transform.position -= new Vector3(2.0f, 0.0f, 2.0f);
            m_doorOpen = false;
        }
    }
}
