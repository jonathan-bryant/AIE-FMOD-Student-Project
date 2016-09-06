using UnityEngine;
using System.Collections;

public class GuidePillar : MonoBehaviour
{
    int m_isActive;
    float m_selectionY;
	void Start () {
        m_selectionY = 3.0f;
        m_isActive = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_isActive == 0)
        {

        }
        else if (m_isActive == 1)
        {
            if (m_selectionY > transform.localPosition.y)
            {
                transform.Translate(0.0f, Time.deltaTime * 2.0f, 0.0f, Space.Self);
                float diff = transform.localPosition.y - m_selectionY;
                if (diff >= 0.0f)
                {
                    transform.Translate(0.0f, -diff, 0.0f, Space.Self);
                    m_isActive = 0;
                }
            }
            else
            {
                transform.Translate(0.0f, -Time.deltaTime * 2.0f, 0.0f, Space.Self);
                float diff = transform.localPosition.y - m_selectionY;
                if (diff <= 0.0f)
                {
                    transform.Translate(0.0f, -diff, 0.0f, Space.Self);
                    m_isActive = 0;
                }
            }
        }
        else
        {
            transform.Translate(0.0f, -Time.deltaTime * 2.0f, 0.0f, Space.Self);
            float diff = transform.localPosition.y - 2.25f;
            if(diff <= 0.0f)
            {
                transform.Translate(0.0f, -diff, 0.0f, Space.Self);
                m_isActive = 0;
            }
        }
	}

    public void Hide()
    {
        if (m_isActive != 0)
            return;
        m_isActive = -1;
    }
    public void Summon(float a_y)
    {
        if (m_isActive != 0)
            return;
        m_isActive = 1;
        m_selectionY = a_y;
    }
}
