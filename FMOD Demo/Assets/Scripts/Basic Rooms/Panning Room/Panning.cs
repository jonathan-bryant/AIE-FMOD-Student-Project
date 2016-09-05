using UnityEngine;
using System.Collections;

public class Panning : MonoBehaviour
{
    bool m_is3D;
    float m_elapsed;
    bool m_isActive;
    public GameObject m_tv;
    public GameObject m_objects;
    float m_originalFOV;

    void Start()
    {
        m_originalFOV = Camera.main.fieldOfView;
        m_isActive = false;
        m_elapsed = 0.0f;
        m_is3D = true;
        m_tv.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_isActive = true;
        }
        if (m_isActive)
        {
            m_elapsed += Time.deltaTime;
            Camera.main.fieldOfView = m_originalFOV + Mathf.Sin(m_elapsed * 3.0f) * 40.0f;
            for (int i = 0; i < m_objects.transform.childCount; ++i)
            {
                FMODUnity.StudioEventEmitter em = m_objects.transform.GetChild(i).gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
                if (!m_is3D)
                {
                    em.SetParameter("Panning", m_elapsed);
                }
                else
                {
                    em.SetParameter("Panning", 1.0f - m_elapsed);
                }
            }

            if (m_elapsed >= 1.0f)
            {
                Camera.main.fieldOfView = m_originalFOV;
                m_tv.SetActive(!m_tv.activeSelf);
                m_elapsed = 0.0f;
                m_isActive = false;
                m_is3D = !m_is3D;
                for (int i = 0; i < m_objects.transform.childCount; ++i)
                {
                    m_objects.transform.GetChild(i).gameObject.SetActive(m_is3D);
                }
            }
        }
    }
}