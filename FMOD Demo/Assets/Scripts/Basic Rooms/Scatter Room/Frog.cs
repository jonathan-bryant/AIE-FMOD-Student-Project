using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour
{
    public float m_turningPower;
    public float m_jumpInterval;
    public float m_jumpDistance;
    public float m_jumpHeight;

    float m_elapsed;

    bool m_isJumping;
    Vector3 m_newPosition;
    Vector3 m_oldPosition;

    void Start()
    {

    }
    void Update()
    {
        if (m_isJumping)
        {
            transform.forward = Quaternion.Euler(0.0f, m_turningPower, 0.0f) * transform.forward;
            transform.position += transform.forward * m_jumpDistance * Time.deltaTime / m_jumpHeight;
            Vector3 pos = transform.position;
            //pos.y = Mathf.Sin(m_elapsedInterval / m_jumpHeight) * m_jumpHeight;
        }
        else
        {
            m_elapsed += Time.deltaTime;
            if (m_elapsed >= m_jumpInterval)
            {
                m_isJumping = true;
                //m_oldPosition = 
            }
        }
    }
    void OnCollisionEnter(Collision a_col)
    {
        if (m_isJumping)
        {
            if (a_col.gameObject.tag == "Ground")
            {
                m_elapsed = 0.0f;
                m_isJumping = false;
            }
        }
    }
}