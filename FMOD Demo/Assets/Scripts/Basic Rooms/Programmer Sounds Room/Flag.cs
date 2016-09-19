/*===============================================================================================
|  Project:		FMOD Demo                                                                       |
|  Developer:	Matthew Zelenko                                                                 |
|  Company:		FMOD                                                                            |
|  Date:		20/09/2016                                                                      |
================================================================================================*/
using UnityEngine;
using System.Collections;

public class Flag : ActionObject
{
    public string m_sound;
    bool m_canCollide, m_isActive;
    float m_distance;
    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_distance = 0.0f;
        m_canCollide = true;
        m_isActive = false;
    }
    void Update()
    {
        if(m_isActive)
        {
            RaycastHit rh;
            int layerMask = 1 << 10;
            layerMask = ~layerMask;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rh, m_distance, layerMask))
            {

                transform.position = Camera.main.transform.position + (Camera.main.transform.forward * rh.distance);
            }
            else
            {
                transform.position = Camera.main.transform.position + (Camera.main.transform.forward * m_distance);
            }
        }
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        Flag[] flags = (Flag[])GameObject.FindObjectsOfType(typeof(Flag));
        foreach (Flag f in flags)
        {
            if (f.m_isActive && f != this)
            {
                Vector3 pos = f.transform.position;
                if (pos.y <= sender.transform.position.y - sender.transform.localScale.y)
                {
                    pos.y = sender.transform.position.y - sender.transform.localScale.y;
                    f.transform.position = pos;
                }

                f.m_isActive = false;
                f.m_rb.useGravity = true;
                f.m_canCollide = true;
                break;
            }
        }
        m_distance = (transform.position - Camera.main.transform.position).magnitude;
        m_isActive = !m_isActive;
        m_rb.useGravity = !m_rb.useGravity;
        if (!m_isActive)
        {
            m_canCollide = true;
            Vector3 pos = transform.position;
            if (pos.y <= sender.transform.position.y - sender.transform.localScale.y)
            {
                pos.y = sender.transform.position.y - sender.transform.localScale.y;
                transform.position = pos;
            }
        }
    }

    void OnCollisionStay(Collision a_col)
    {
        if (m_canCollide)
        {
            if (a_col.gameObject.name == "DialogueBox")
            {
                Dialogue dialogue = a_col.gameObject.GetComponent<Dialogue>();
                if (dialogue)
                {
                    dialogue.PlayDialogue(m_sound);
                    m_canCollide = false;
                }
            }
        }
    }
}