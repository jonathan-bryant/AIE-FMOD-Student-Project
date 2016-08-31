/*=================================================================
Project:		AIE FMOD
Developer:		Cameron baron
Company:		FMOD
Date:			30/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.EventSystems;

public class InformationColumnScript : MonoBehaviour
{
    // Public Vars
    public string m_defaultAnimation = "Take 001";
    public Material m_defaultTextMat;
    public Material m_textHighlightedMat;
    public Material m_textVisitedMat;

    // Private Vars
    [SerializeField]    GameObject m_pillarInner;
    [SerializeField]    float m_idleRadius = 20.0f;
    float m_distanceToPlayer;
    GameObject m_playerRef;
    InformationPillarText[] m_pillarTexts;
    bool m_defaultAnimPlaying = true;

    int m_currentSelection = 0;

	void Start () 
	{
        m_playerRef = GameObject.FindGameObjectWithTag("Player");
        m_pillarTexts = GetComponentsInChildren<InformationPillarText>();
    }
	
	void Update () 
	{
        m_distanceToPlayer = Vector3.Distance(m_playerRef.transform.position, transform.position);
        if (m_distanceToPlayer <= m_idleRadius)
        {
            Vector3 lookAtPos = new Vector3(m_playerRef.transform.position.x, m_pillarInner.transform.position.y, m_playerRef.transform.position.z);
            m_pillarInner.transform.LookAt(lookAtPos, Vector3.up);
            m_pillarTexts[m_currentSelection].GetComponentInChildren<Renderer>().material = m_textHighlightedMat;
            if (m_defaultAnimPlaying)
            {
                for (int i = 0; i < m_pillarTexts.Length; i++)
                {
                    m_pillarTexts[i].SetAnimationSpeed(0);
                }
                m_defaultAnimPlaying = false;
            }
            else
            {
                if (Input.mouseScrollDelta.y > 0.1f)
                {
                    m_pillarTexts[m_currentSelection].GetComponentInChildren<Renderer>().material = m_defaultTextMat;
                    m_currentSelection += 1;
                    if (m_currentSelection >= m_pillarTexts.Length)
                    {
                        m_currentSelection = 0;
                    }
                    
                    m_pillarTexts[m_currentSelection].GetComponentInChildren<Renderer>().material = m_textHighlightedMat;
                }
                else if (Input.mouseScrollDelta.y < -0.1f)
                {
                    m_pillarTexts[m_currentSelection].GetComponentInChildren<Renderer>().material = m_defaultTextMat;
                    m_currentSelection -= 1;
                    if (m_currentSelection < 0)
                    {
                        m_currentSelection = m_pillarTexts.Length - 1;
                    }

                    m_pillarTexts[m_currentSelection].GetComponentInChildren<Renderer>().material = m_textHighlightedMat;
                }
            }
        }
        else if (m_distanceToPlayer > m_idleRadius && m_defaultAnimPlaying == false)
        {
            PlayDefaultAnimation();
            // stop default animation
        }
        //Input.mouseScrollDelta.y
    }

	#region Private Functions

    void PlayDefaultAnimation()
    {
        for (int i = 0; i < m_pillarTexts.Length; i++)
        {
            if (i < m_pillarTexts.Length / 2)
            {
                m_pillarTexts[i].PlayAnim((float)i / 10.0f);
            }
            else
            {
                m_pillarTexts[i].PlayAnim((float)(i - m_pillarTexts.Length / 2) / 10.0f);
            }

            m_pillarTexts[i].GetComponent<InformationPillarText>().animSpeed = 1;
        }
        m_defaultAnimPlaying = true;
    }

	#endregion
}
