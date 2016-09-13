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
            transform.position = Camera.main.transform.position + (Camera.main.transform.forward * m_distance);
        }
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_distance = (transform.position - Camera.main.transform.position).magnitude;
        m_isActive = !m_isActive;
        m_rb.isKinematic = !m_rb.isKinematic;
        if (!m_isActive)
            m_canCollide = true;
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