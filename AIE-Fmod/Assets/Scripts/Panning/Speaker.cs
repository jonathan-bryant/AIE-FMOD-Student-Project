using UnityEngine;
using System.Collections;

public class Speaker : MonoBehaviour
{
    public PanButton m_panButton;
    // Use this for initialization
    float m_elapsedPulsate;
    Vector3 m_originalScale;
    public bool m_flip;

    void Start()
    {
        m_originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        bool m_panEnable = m_panButton.PanEnabled;
        if (m_flip)
            m_panEnable = !m_panEnable;

        if(!m_panEnable)
        {
            m_elapsedPulsate += Time.deltaTime;
            transform.localScale = m_originalScale * (Mathf.Sin(m_elapsedPulsate * 4.0f) * 0.25f + 1.0f);
        }
        else
        {
            transform.localScale = m_originalScale;
        }
    }
}