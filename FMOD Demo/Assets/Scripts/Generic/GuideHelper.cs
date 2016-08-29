using UnityEngine;
using System.Collections;

public class GuideHelper : ActionObject
{
    public Guide m_spinGuide;
    public Guide m_interactiveMusicGuide;
    public Guide m_snapshotGuide;
    public Guide m_playerParamGuide;

    void Start ()
    {
	}
	void Update ()
    {

	}

    public override void ActionDown(GameObject sender)
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_spinGuide.Play();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_interactiveMusicGuide.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_snapshotGuide.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_playerParamGuide.Play();
        }
    }
}