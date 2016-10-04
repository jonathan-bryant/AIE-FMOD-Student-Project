using UnityEngine;
using System.Collections;

public class VolumeSlider : ActionObject
{
    public Vector3 m_startPosition;
    public Vector3 m_endPosition;
    float m_slideValue;

    bool m_isActive;

    [FMODUnity.EventRef]
    public string m_eventPath;
    FMOD.Studio.EventInstance m_event;
    Transform m_soundPosition;

    void Start()
    {
        InitGlow();
        m_slideValue = 0.0f;
        m_event = FMODUnity.RuntimeManager.CreateInstance(m_eventPath);
        m_event.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_event, m_soundPosition, null);

        float diff = (transform.position - m_startPosition).magnitude / (m_endPosition - m_startPosition).magnitude;
        diff = Mathf.Clamp(diff, 0.0f, 1.0f);
        m_event.setVolume(Mathf.Lerp(0.0f,1.0f, diff));
    }
    void Update()
    {
        UpdateGlow();
        if (Input.GetKeyUp(m_actionKeys[0]))
        {
            m_isActive = false;
        }
        if (m_isActive)
        {
            RaycastHit rh;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rh, Mathf.Infinity, ~(1 << 10 | 1 << 2)))
            {
                if (rh.collider.gameObject.name == "Radio")
                {
                    float diff = 0.0f;
                    float dot = Vector3.Dot((rh.point - m_startPosition).normalized, (m_endPosition - m_startPosition).normalized);
                    if (dot > 0)
                    {
                        diff = (rh.point - m_startPosition).magnitude / (m_endPosition - m_startPosition).magnitude;
                    }
                    transform.position = Vector3.Lerp(m_startPosition, m_endPosition, diff * dot);
                    m_event.setVolume(Mathf.Lerp(0.0f, 1.0f, diff * dot));
                }
            }
        }
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_isActive = true;
    }

    void OnDestroy()
    {
        m_event.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        m_event.release();
    }
}