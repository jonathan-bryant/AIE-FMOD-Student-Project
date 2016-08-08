using UnityEngine;
using System.Collections;

public class RainController : MonoBehaviour
{
    public ActorControls m_actor;
    public ParticleSystem m_particleSystem;
    public float m_rainValue;

    bool m_active;
    float m_originalRate;
    float m_originalSpeed;

    void Start()
    {
        m_originalRate = m_particleSystem.emission.rate.constantMax;
        m_originalSpeed = m_particleSystem.startSpeed;

        var emission = m_particleSystem.emission;

        var rate = emission.rate;
        rate.constantMax = Mathf.Lerp(0, m_originalRate, m_rainValue);
        emission.rate = rate;

        m_particleSystem.startSpeed = Mathf.Lerp(0.0f, m_originalSpeed, m_rainValue);

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
                if (info.collider.name == "Rain Knob")
                {
                    m_actor.Disabled = true;
                    m_active = true;
                }
            }
        }
        if (Input.GetMouseButton(0) && m_active)
        {
            float mouseX = Input.GetAxis("Mouse X");
            m_rainValue += mouseX / 50.0f;
            m_rainValue = Mathf.Clamp(m_rainValue, 0.0f, 1.0f);
            transform.Rotate(new Vector3(0.0f, -mouseX * 10.0f, 0.0f));

            var emission = m_particleSystem.emission;

            var rate = emission.rate;
            rate.constantMax = Mathf.Lerp(0, m_originalRate, m_rainValue);
            emission.rate = rate;
            
            m_particleSystem.startSpeed = Mathf.Lerp(0.0f, m_originalSpeed, m_rainValue);
        }
        if (Input.GetMouseButtonUp(0) && m_active)
        {
            m_actor.Disabled = false;
            m_active = false;
        }
    }
}
