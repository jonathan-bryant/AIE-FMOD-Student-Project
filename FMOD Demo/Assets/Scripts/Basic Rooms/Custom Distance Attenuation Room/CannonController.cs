using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour
{

    public GameObject m_cannonBall;
    public GameObject m_cannon;
    float m_currentAngle;
    float m_power;
    float m_selectedAngle;
    bool m_isActive;

    void Start()
    {
        m_currentAngle = 30.0f;
        m_selectedAngle = 30.0f;
        m_power = 10.0f;
        m_isActive = false;
    }
    void FixedUpdate()
    {
        if (m_isActive)
        {
            if (m_currentAngle != m_selectedAngle)
            {
                if (m_selectedAngle < m_currentAngle)
                {
                    m_currentAngle -= 0.02f;
                }
                else
                {
                    m_currentAngle += 0.02f;
                }
            }
            else
            {
                m_isActive = false;
                GameObject ball = Instantiate(m_cannonBall, m_cannon.transform.GetChild(0).position - (m_cannon.transform.GetChild(0).up), Quaternion.identity) as GameObject;
                ball.transform.SetParent(transform);
                ball.GetComponent<Rigidbody>().AddForce(-m_cannon.transform.GetChild(0).up * m_power, ForceMode.Impulse);
            }
        }
    }
    void Update()
    {

    }
    public void Fire(int a_index)
    {
        if (m_isActive)
            return;

        switch (a_index)
        {
            case 1:
                m_selectedAngle = 30.0f;
                m_power = 10.0f;
                m_isActive = true;
                break;
            case 2:
                m_selectedAngle = 45.0f;
                m_power = 12.5f;
                m_isActive = true;
                break;
            case 3:
                m_selectedAngle = 60.0f;
                m_power = 15.0f;
                m_isActive = true;
                break;
            default:
                break;
        }
    }
}
