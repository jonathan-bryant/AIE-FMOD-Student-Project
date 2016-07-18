using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    bool m_doorOpen;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
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
