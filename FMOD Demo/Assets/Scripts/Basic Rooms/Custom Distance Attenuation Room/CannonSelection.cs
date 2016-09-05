using UnityEngine;
using System.Collections;

public class CannonSelection : ActionObject
{
    public int m_selection;
    public CannonController m_controller;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActionPressed(GameObject sender)
    {
        m_controller.Fire(m_selection);
    }
}
