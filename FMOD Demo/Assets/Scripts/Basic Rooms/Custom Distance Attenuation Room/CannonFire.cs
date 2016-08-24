using UnityEngine;
using System.Collections;

public class CannonFire : ActionObject
{
    public CannonController m_controller;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActionPressed(GameObject sender)
    {
        m_controller.Fire();
    }
}
