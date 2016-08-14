using UnityEngine;
using System.Collections;

public class InteractiveButtons : MonoBehaviour
{
    ActorControls m_actor;
    public FMODUnity.StudioEventEmitter m_emitter;
    public GameObject[] m_buttons;

    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
    }

    void Update()
    {
        foreach (GameObject g in m_buttons)
        {
            g.GetComponent<Renderer>().material.SetInt("_OutlineEnabled", 0);
        }
        RaycastHit info;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out info, 10.0f))
        {
            info.collider.gameObject.GetComponent<Renderer>().material.SetInt("_OutlineEnabled", 1);
            if (Input.GetMouseButtonDown(0))
            {
                bool hit = false;
                if (info.collider.name == "Low")
                {
                    m_emitter.SetParameter("Intensity", 0.20f);
                    hit = true;
                }
                if (info.collider.name == "Mid")
                {
                    m_emitter.SetParameter("Intensity", 0.40f);
                    hit = true;
                }
                if (info.collider.name == "High")
                {
                    m_emitter.SetParameter("Intensity", 0.60f);
                    hit = true;
                }
                if (info.collider.name == "End")
                {
                    m_emitter.SetParameter("Intensity", 0.80f);
                    hit = true;
                }
                if(hit)
                {
                    foreach (GameObject g in m_buttons)
                    {
                        g.SetActive(true);
                    }
                    info.collider.gameObject.SetActive(false);
                }
            }
        }
    }
}