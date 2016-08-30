using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {
    public GameObject m_cannonBall;
    public GameObject m_cannon;
    // Use this for initialization
    float m_angle;
    float m_speed;
    float m_fireRate, m_fireRateElapsed;
    public float m_power;
	void Start () {
        m_angle = 30.0f;
        m_speed = 25.0f;
        m_fireRate = 3.0f;
        m_fireRateElapsed = m_fireRate;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_fireRateElapsed < m_fireRate)
        {
            m_fireRateElapsed += Time.deltaTime;
        }
    }
    public void Fire()
    {
        if(m_fireRateElapsed >= m_fireRate)
        {
            GameObject obj = (GameObject)Instantiate(m_cannonBall, m_cannon.transform.position + (-m_cannon.transform.GetChild(0).up * 1.75f), Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(-m_cannon.transform.GetChild(0).up * m_power, ForceMode.VelocityChange);
            obj.transform.SetParent(transform);
            m_fireRateElapsed = 0.0f;
        }
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
