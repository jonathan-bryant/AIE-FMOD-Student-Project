using UnityEngine;
using System.Collections;

public class Panning_Robot : MonoBehaviour
{
    int m_isActive;
    float m_elapsed;
    FMODUnity.StudioEventEmitter m_event;
    Transform m_actor;
    Quaternion m_oldRotation;

	void Start ()
    {
        m_event = GetComponent<FMODUnity.StudioEventEmitter>();
        m_oldRotation = transform.rotation;
        m_isActive = 0;
        m_elapsed = 0.0f;
        m_actor = Camera.main.transform.parent;
	}
	void Update ()
    {
        if(m_isActive == 1)
        {
            m_elapsed += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(m_actor.position - transform.position, Vector3.up), 40.0f * Time.deltaTime);
            if (m_elapsed >= 6.0f)
                m_isActive = -1;
        }
        else if(m_isActive == -1)
        {
            m_elapsed -= Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_oldRotation, 140.0f * Time.deltaTime);
            if (m_elapsed <= 4.0f)
            {
                m_isActive = 0;
                m_event.Play();
            }
        }
	}

    public void FacePlayer()
    {
        if (m_isActive != 0)
            return;
        m_isActive = 1;
        m_elapsed = 0.0f;
        m_event.Stop();
        
    }
}