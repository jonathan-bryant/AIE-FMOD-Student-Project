using UnityEngine;
using System.Collections;

public class TransceiverToggle : ActionObject
{
    FMODUnity.StudioEventEmitter m_eventEmitter;
    void Start()
    {
        InitGlow();
        m_eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {
        UpdateGlow();
    }
    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        if (m_eventEmitter.IsPlaying())
        {
            m_eventEmitter.SetParameter("On", 0.0f);
            m_eventEmitter.Stop();
        }
        else
        {
            m_eventEmitter.Play();
            m_eventEmitter.SetParameter("On", 1.0f);
        }
    }
}