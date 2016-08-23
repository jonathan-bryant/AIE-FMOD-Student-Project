using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {

    public GameObject m_cannon;
    // Use this for initialization
    float m_angle;
    float m_speed;
	void Start () {
        m_angle = 30.0f;
        m_speed = 100.0f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LowerCannon()
    {
        if (m_angle < 60.0f)
        {
            m_angle += m_speed * Time.deltaTime;
            if (m_angle > 60.0f)
            {
                m_angle = 60.0f;
            }
            Vector3 angle = m_cannon.transform.eulerAngles;
            angle.x = m_angle;
            angle.z = 0.0f;
            m_cannon.transform.eulerAngles = angle;
        }
    }
    public void RaiseCannon()
    {
        if (m_angle > 30.0f)
        {
            m_angle -= m_speed * Time.deltaTime;
            if (m_angle < 30.0f)
            {
                m_angle = 30.0f;
            }
            Vector3 angle = m_cannon.transform.eulerAngles;
            angle.x = m_angle;
            angle.z = 0.0f;
            m_cannon.transform.eulerAngles = angle;
        }
    }
}
