using UnityEngine;
using System.Collections;

public class GuideText : ActionObject
{
    public Guide m_guide;

	void Start () {
	
	}
	void Update () {
	
	}

    public override void ActionPressed(GameObject sender)
    {
        m_guide.Play();
    }
}