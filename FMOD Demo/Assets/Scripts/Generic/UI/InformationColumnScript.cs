/*=================================================================
Project:		AIE FMOD
Developer:		Cameron baron
Company:		FMOD
Date:			30/08/2016
==================================================================*/

using UnityEngine;
using System.Collections.Generic;

public class InformationColumnScript : MonoBehaviour 
{
    // Public Vars
    public string m_defaultAnimation = "Take 001";

    // Private Vars
    [SerializeField]    GameObject m_pillarInner;
    [SerializeField]    float m_idleRadius = 20.0f;
    float m_distanceToPlayer;
    GameObject m_playerRef;
    Animator[] m_textAnimators;
    bool m_defaultAnimPlaying = true;

	void Start () 
	{
        m_playerRef = GameObject.FindGameObjectWithTag("Player");
        m_textAnimators = GetComponentsInChildren<Animator>();
	}
	
	void Update () 
	{
        m_distanceToPlayer = Vector3.Distance(m_playerRef.transform.position, transform.position);
        if (m_distanceToPlayer <= m_idleRadius)
        {
            Vector3 lookAtPos = new Vector3(m_playerRef.transform.position.x, m_pillarInner.transform.position.y, m_playerRef.transform.position.z);
            m_pillarInner.transform.LookAt(lookAtPos, Vector3.up);

            for (int i = 0; i < m_textAnimators.Length; i++)
            {
                m_textAnimators[i].GetComponent<InformationPillarText>().animSpeed = 0;
            }

                if (m_defaultAnimPlaying)
            {
                // stop default animation
            }
        }
        else if (m_distanceToPlayer > m_idleRadius && m_defaultAnimPlaying == false)
        {
            for (int i = 0; i < m_textAnimators.Length; i++)
            {
                m_textAnimators[i].GetComponent<InformationPillarText>().animSpeed = 1;
            }
            // play default animation
        }
	}

	#region Private Functions

    void PlayDefaultAnimation()
    {
        if (m_distanceToPlayer > m_idleRadius)
        {
            for (int i = 0; i < m_textAnimators.Length; i++)
            {
                if (i < m_textAnimators.Length / 2)
                {
                    m_textAnimators[i].Play(m_defaultAnimation, 0, i / 10);
                    m_textAnimators[i].GetComponent<InformationPillarText>().animSpeed = 1;
                }
                else
                {
                    m_textAnimators[i].Play(m_defaultAnimation, 0, (i - m_textAnimators.Length / 2) / 10);
                    m_textAnimators[i].GetComponent<InformationPillarText>().animSpeed = 1;
                }
            }
        }
    }

	#endregion
}
