/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Programmer Sounds                                               |
|   Fmod Related Scripting:     No                                                              |
|   Description:                When the flag has been acted upon. It will follow the actors    |
|   line of sight until action key is pressed again or until the actor has picked up another    |
|   flag.                                                                                       |
===============================================================================================*/
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
            //Raycast past flag to see if theres obstruction forcing the flag to be closer to the player
            RaycastHit rh;
            int layerMask = 1 << LayerMask.NameToLayer("PlayerIgnore");
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
        //Handling picking up another flag while alreay flag.
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
        //When a collisoin happens with the flag and the dialogue box, play the dialogue from the dialogue script.
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