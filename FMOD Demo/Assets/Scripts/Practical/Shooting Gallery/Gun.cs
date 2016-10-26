/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shootimg Gallery                                                |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Shoots bullets.                                                 |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject m_bullet;
    public Transform m_gunHole;
    public float m_power = 2.0f;
    public float m_fireRate = 0.1f;

    /*===============================================FMOD====================================================
    |   Storing the shoot sound event name so we can use PlayOneShot.                                       |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    public string m_eventRef;

    float m_elapsed;
    
    void Start()
    {
        m_elapsed = 0.0f;
    }
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        m_elapsed = Mathf.Min(m_elapsed + Time.deltaTime, m_fireRate);
        if (Input.GetMouseButton(0) && m_elapsed >= m_fireRate)
        {
            /*===============================================FMOD====================================================
            |   This is how you play a oneshot sound quickly. Optionally a transform can be passed into this        |
            |   function.                                                                                           |
            =======================================================================================================*/
            FMODUnity.RuntimeManager.PlayOneShot(m_eventRef);

            GameObject obj = Instantiate(m_bullet) as GameObject;
            obj.transform.position = m_gunHole.position;
            obj.transform.forward = transform.forward;
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * m_power);
            m_elapsed = 0.0f;
        }
    }
}