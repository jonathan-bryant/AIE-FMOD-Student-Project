/*===============================================================================================
|  Project:		FMOD Demo                                                                       |
|  Developer:	Matthew Zelenko                                                                 |
|  Company:		FMOD                                                                            |
|  Date:		20/09/2016                                                                      |
================================================================================================*/
using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    public Orchestrion m_orchestrion;
    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter(Collider a_col)
    {
        Debug.Log("Collision");
        m_orchestrion.Pause();
    }
}