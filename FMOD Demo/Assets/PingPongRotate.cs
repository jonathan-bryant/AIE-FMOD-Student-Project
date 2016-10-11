using UnityEngine;
using System.Collections;

public class PingPongRotate : MonoBehaviour
{
    public Vector3 m_rotationAxis;
    public float m_rotationAngle;
    public float m_swingDuration;
    Vector3 m_originalForward;
    public bool m_flicker;
    Light m_light;
    float m_lightIntensity;
    int m_count;

	void Start ()
    {
        m_light = GetComponentInChildren<Light>();
        m_rotationAxis = m_rotationAxis.normalized;
        m_originalForward = transform.forward;
        m_lightIntensity = m_light.intensity;
	}
    void FixedUpdate()
    {
        if (!m_flicker)
            return;
        if (m_count == 5)
        {
            int rng = Random.Range(1, 100);
            if (rng <= 20)
            {
                m_light.intensity = m_lightIntensity * 0.4f;
            }
            else
            {
                m_light.intensity = m_lightIntensity;
            }
            m_count = 0;
        }
        m_count++;
    }
	void Update ()
    {
        Quaternion rot = Quaternion.AngleAxis(Mathf.Sin(Time.time * (m_swingDuration == 0 ? 0 : (1 / m_swingDuration))) * m_rotationAngle, m_rotationAxis);
        transform.forward = rot * m_originalForward;
	}
}
