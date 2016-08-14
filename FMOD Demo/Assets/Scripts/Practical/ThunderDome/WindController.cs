﻿using UnityEngine;
using System.Collections;

public class WindController : MonoBehaviour
{
    ActorControls m_actor;
    public ParticleSystem m_particleSystem;

    public float m_windValue;

    float m_orignialX;
    bool m_active;

    Material m_material;

    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
        m_material = GetComponent<Renderer>().material;
        m_active = false;
        var vol = m_particleSystem.forceOverLifetime;
        var x = vol.x;
        m_orignialX = x.constantMax;
        x.constantMax = 0.0f;
        vol.x = x;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit info;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out info, 10.0f))
        {
            if (info.collider.name == "Wind Knob")
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
            m_windValue += mouseX / 50.0f;
            m_windValue = Mathf.Clamp(m_windValue, 0.0f, 1.0f);
            transform.Rotate(new Vector3(0.0f, -mouseX * 10.0f, 0.0f));

            var vol = m_particleSystem.forceOverLifetime;
            var x = vol.x;
            x.constantMax = Mathf.Lerp(0.0f, m_orignialX, m_windValue);
            vol.x = x;
        }
        if (Input.GetMouseButtonUp(0) && m_active)
        {
            m_actor.Disabled = false;
            m_active = false;
        }
    }
}
