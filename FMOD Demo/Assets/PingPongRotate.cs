using UnityEngine;
using System.Collections;

public class PingPongRotate : MonoBehaviour
{
    public Vector3 m_rotationAxis;
    public float m_rotationAngle;
    public float m_swingDuration;
    Vector3 m_originalForward;

	void Start ()
    {
        m_rotationAxis = m_rotationAxis.normalized;
        m_originalForward = transform.forward;
	}
	void Update ()
    {
        Quaternion rot = Quaternion.AngleAxis(Mathf.Sin(Time.time * (m_swingDuration == 0 ? 0 : (1 / m_swingDuration))) * m_rotationAngle, m_rotationAxis);
        transform.forward = rot * m_originalForward;
	}
}
