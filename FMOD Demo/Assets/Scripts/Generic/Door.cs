using UnityEngine;
using System.Collections;

public class Door : ActionObject
{
    bool m_doorOpen;
    public bool DoorOpen { get { return m_doorOpen; } }

    void Start()
    {
        

    }

    void Update()
    {

    }

    public override void Use(bool a_use)
    {
        if (!m_doorOpen)
        {
            transform.position += transform.right * 2.0f;
            transform.position += transform.forward * 2.0f;
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 90.0f);
            m_doorOpen = true;
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -90.0f);
            transform.position -= transform.right * 2.0f;
            transform.position -= transform.forward * 2.0f;
            m_doorOpen = false;
        }
    }
}
