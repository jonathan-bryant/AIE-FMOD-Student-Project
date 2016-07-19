using UnityEngine;
using System.Collections;

public class PanButton : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter m_bgMusic;

    bool m_enablePan;
    public bool PanEnabled { get { return m_enablePan; }}    
    float m_panElapsed;

    void Start()
    {
        m_panElapsed = 0.0f;
    }
    
    void Update()
    {
        if (m_enablePan)
        {
            m_panElapsed = Mathf.Min(1.0f, m_panElapsed + Time.deltaTime);
            m_bgMusic.SetParameter("Panning", m_panElapsed);
        }
        else
        {
            m_panElapsed = Mathf.Max(0.0f, m_panElapsed - Time.deltaTime);
            m_bgMusic.SetParameter("Panning", m_panElapsed);
        }
    }
    public void Use()
    {
        m_enablePan = !m_enablePan;
    }
}
