using UnityEngine;
using System.Collections;

public class PanButton : ActionObject
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
    protected override void Action(GameObject sender, bool a_use)
    {
        if(a_use)
            m_enablePan = !m_enablePan;
    }
}
