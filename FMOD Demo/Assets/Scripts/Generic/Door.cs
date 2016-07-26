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
            transform.position += transform.right * ((transform.localScale.z * 0.5f) + (transform.localScale.x * 0.5f));
            transform.position += transform.forward * ((transform.localScale.z * 0.5f) + (transform.localScale.x * 0.5f));
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 90.0f);
            m_doorOpen = true;
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -90.0f);
            transform.position -= transform.right * ((transform.localScale.z * 0.5f) + (transform.localScale.x * 0.5f));
            transform.position -= transform.forward * ((transform.localScale.z * 0.5f) + (transform.localScale.x * 0.5f));
            m_doorOpen = false;
        }
    }
}
