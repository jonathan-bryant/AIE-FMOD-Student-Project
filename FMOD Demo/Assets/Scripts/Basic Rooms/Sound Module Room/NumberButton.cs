using UnityEngine;
using System.Collections;

public class NumberButton : ActionObject
{
    public Orchestrion m_orchestrion;
    public int m_number;
    FMODUnity.StudioEventEmitter m_buttonEvent;

    void Start ()
    {
        m_buttonEvent = GetComponent<FMODUnity.StudioEventEmitter>();
	}
	void Update ()
    {
	
	}

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_orchestrion.ChangeSound(m_number);
        m_buttonEvent.Play();
    }
}