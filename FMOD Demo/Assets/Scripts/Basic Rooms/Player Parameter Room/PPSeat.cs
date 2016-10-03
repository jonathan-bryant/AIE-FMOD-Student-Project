using UnityEngine;
using System.Collections;

public class PPSeat : ActionObject
{
    ActorControls m_actor;
    bool m_isSeated, m_isReady;
    public Transform m_exitSeat;

    void Start()
    {
        m_isSeated = false;
        m_isReady = false;
        m_actor = Camera.main.gameObject.GetComponentInParent<ActorControls>();
    }

    void Update()
    {
        if (m_isSeated && m_isReady)
        {
            m_actor.transform.position = transform.position;
            for (int i = 0; i < m_actionKeys.Length; ++i)
            {
                if (Input.GetKeyDown(m_actionKeys[i]))
                {
                    m_isSeated = false;
                    m_actor.transform.position = m_exitSeat.position;
                    m_actor.SetRotation(m_exitSeat.rotation);
                    GetComponent<Collider>().enabled = true;
                    m_actor.GetComponent<CharacterController>().enabled = true;
                    m_actor.DisableMovement = false;
                    return;
                }
            }
        }
        m_isReady = true;
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_isSeated = true;
        m_isReady = false;
        m_actor.transform.position = transform.position;
        m_actor.SetRotation(transform.rotation);
        m_actor.DisableMovement = true;
        m_actor.GetComponent<CharacterController>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}