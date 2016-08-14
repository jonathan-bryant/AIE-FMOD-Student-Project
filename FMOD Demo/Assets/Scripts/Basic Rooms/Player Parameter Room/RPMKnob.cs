using UnityEngine;
using System.Collections;

public class RPMKnob : MonoBehaviour
{
    ActorControls m_actor;
    public FMODUnity.StudioEventEmitter m_emitter;

    Material m_material;
    bool m_active;
    float m_rpmValue;

    // Use this for initialization
    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
        m_material = GetComponent<Renderer>().material;
        m_active = false;
        m_rpmValue = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit info;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out info, 10.0f))
        {
            if (info.collider.name == "RPM Knob")
            {
                m_material.SetInt("_OutlineEnabled", 1);
                if (Input.GetMouseButtonDown(0))
                {
                    m_actor.Disabled = true;
                    m_active = true;
                }
            }
            else
            {
                m_material.SetInt("_OutlineEnabled", 0);
            }
        }
        if (Input.GetMouseButton(0) && m_active)
        {
            float mouseX = Input.GetAxis("Mouse X");
            m_rpmValue += mouseX * 10.0f;
            m_rpmValue = Mathf.Clamp(m_rpmValue, 0.0f, 2000.0f);
            transform.Rotate(new Vector3(0.0f, -mouseX * 10.0f, 0.0f));
            m_emitter.SetParameter("RPM", m_rpmValue);
        }
        if (Input.GetMouseButtonUp(0) && m_active)
        {
            m_actor.Disabled = false;
            m_active = false;
        }
    }
}