using UnityEngine;
using System.Collections;

public class WindController : MonoBehaviour
{
    public ActorControls m_actor;

    public float m_windValue;

    bool m_active;

    // Use this for initialization
    void Start()
    {
        m_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit info;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out info, 10.0f))
            {
                if (info.collider.name == "Wind Knob")
                {
                    m_actor.Disabled = true;
                    m_active = true;
                }
            }
        }
        if (Input.GetMouseButton(0) && m_active)
        {
            float mouseX = Input.GetAxis("Mouse X");
            m_windValue += mouseX / 50.0f;
            m_windValue = Mathf.Clamp(m_windValue, 0.0f, 1.0f);
            transform.Rotate(new Vector3(0.0f, -mouseX * 10.0f, 0.0f));
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_active = false;
        }
    }
}
