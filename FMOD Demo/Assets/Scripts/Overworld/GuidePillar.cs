using UnityEngine;
using System.Collections;

public class GuidePillar : MonoBehaviour
{
    int m_isActive;
    float m_selectionY;
    int m_direction;

	void Start () {
        m_selectionY = 3.0f;
        m_isActive = 0;
        m_direction = 0;
    }
	void Update () {
        if (m_isActive == 1)
        {
            if (m_direction == 1)
            {
                transform.Translate(0.0f, Time.deltaTime * 2.0f, 0.0f, Space.Self);
                Vector3 pos = transform.position;
                if (pos.y >= m_selectionY)
                {
                    pos.y = m_selectionY;
                    transform.position = pos;
                    m_isActive = 0;
                }
            }
            else
            {
                transform.Translate(0.0f, -Time.deltaTime * 2.0f, 0.0f, Space.Self);
                Vector3 pos = transform.position;
                if (pos.y <= m_selectionY)
                {
                    pos.y = m_selectionY;
                    transform.position = pos;
                    m_isActive = 0;
                }
            }
        }
        else if(m_isActive == -1)
        {
            transform.Translate(0.0f, -Time.deltaTime * 2.0f, 0.0f, Space.Self);
            Vector3 pos = transform.position;
            if(pos.y <= -1.175f)
            {
                pos.y = -1.175f;
                transform.position = pos;
                m_isActive = 0;
            }
        }
	}

    public void Hide()
    {
        if (m_isActive != 0)
            return;
        m_isActive = -1;
        m_direction = -1;
    }
    public void Summon(float a_y)
    {
        if (m_isActive != 0)
            return;
        m_isActive = 1;
        m_selectionY = a_y;
        if (m_selectionY > transform.position.y)
            m_direction = 1;
        else
            m_direction = -1;
    }
}
