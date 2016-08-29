using UnityEngine;
using System.Collections;

public class GuideHelper : ActionObject
{
    public Guide m_spinGuide;
    public Guide m_interactiveMusicGuide;
    public Guide m_snapshotGuide;
    public Guide m_playerParamGuide;
    public Guide m_3DAttenuationGuide;
    public Guide m_programmerSoundGuide;
    public Guide m_panningGuide;
    public Guide m_scatterGuide;
    public Guide m_surfacesGuide;
    public Guide m_eventConeGuide;
    public Guide m_transceiverGuide;
    public Guide m_soundModuleGuide;

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
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_3DAttenuationGuide.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            m_programmerSoundGuide.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            m_panningGuide.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            m_scatterGuide.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            m_surfacesGuide.Play();
        }
    }
}