using UnityEngine;
using System.Collections;

public class Panning : MonoBehaviour
{
    bool m_is3D;
    float m_elapsed;
    bool m_isActive;
    public GameObject m_tvs;
    public GameObject m_objects;
    float m_originalFOV, m_originalNear;

    void Start()
    {
        m_originalFOV = Camera.main.fieldOfView;
        m_originalNear = Camera.main.nearClipPlane;
        m_isActive = false;
        m_elapsed = 0.0f;
        m_is3D = true;
        for (int i = 0; i < m_tvs.transform.childCount; ++i)
        {
            m_tvs.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_isActive = true;
            Camera.main.nearClipPlane = 0.02f;
        }
        if (m_isActive)
        {
            m_elapsed += Time.deltaTime;
            if (m_elapsed <= 0.9f)
            {
                Camera.main.fieldOfView += 125.0f * Time.deltaTime;
            }
            else
            {
                Camera.main.fieldOfView -= 900.0f * Time.deltaTime;
            }
            for (int i = 0; i < m_objects.transform.childCount; ++i)
            {
                FMODUnity.StudioEventEmitter em = m_objects.transform.GetChild(i).gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
                if (!m_is3D)
                {
                    em.SetParameter("Panning", m_elapsed / 1.0f);
                }
                else
                {
                    em.SetParameter("Panning", 1.0f - (m_elapsed / 1.0f));
                }
            }

            if (m_elapsed >= 1.0f)
            {
                Camera.main.fieldOfView = m_originalFOV;
                Camera.main.nearClipPlane = m_originalNear;
                m_elapsed = 0.0f;
                m_isActive = false;
                m_is3D = !m_is3D;

                for (int i = 0; i < m_tvs.transform.childCount; ++i)
                {
                    m_tvs.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(!m_is3D);
                }

                Vector3 pos = m_objects.transform.localPosition;
                if (!m_is3D)
                {
                    pos.x = -20.0f;
                }
                else
                {
                    pos.x = 0.0f;
                }
                m_objects.transform.localPosition = pos;
                if (m_is3D)
                {
                    for (int i = 0; i < m_objects.transform.childCount; ++i)
                    {
                        m_objects.transform.GetChild(i).GetComponent<Panning_Robot>().FacePlayer();
                    }
                }
            }
        }
    }
}