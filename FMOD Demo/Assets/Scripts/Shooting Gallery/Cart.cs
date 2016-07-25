using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cart : MonoBehaviour {

    public Track m_trackHolder;
    Vector3 m_newHeading;
    public float m_topSpeed = 3.0f;
    public float m_acceleration = 30.0f;
    public float m_turningPower = 0.4f;
    Vector3 m_currentVelocity;
    List<Transform> m_tracks;
    int m_currentTrackIndex;
    public Transform m_seat;

	void Start () {
        m_tracks = new List<Transform>();
        //Add children to m_tracks(list of transforms)
        for(int i = 0; i < m_trackHolder.transform.childCount; ++i)
        {
            m_tracks.Add(m_trackHolder.transform.GetChild(i));
        }
        m_currentTrackIndex = m_trackHolder.m_startingIndex;
        transform.position = new Vector3(m_tracks[m_currentTrackIndex].transform.position.x, transform.position.y, m_tracks[m_currentTrackIndex].transform.position.z);
        transform.forward = m_tracks[++m_currentTrackIndex].transform.position - transform.position;
	}
	
	void Update () {
       
	}
    void FixedUpdate()
    {
        CalculateHeading();
        Move();
    }

    void CalculateHeading()
    {
        //Get track
        if((m_tracks[m_currentTrackIndex].transform.position - transform.position).magnitude <= 1.0f)
        {
            ++m_currentTrackIndex;
            if (m_currentTrackIndex >= m_trackHolder.transform.childCount)
                m_currentTrackIndex = 0;
        }

        //Turn towards next track
        Vector3 newForward = transform.forward + (m_tracks[m_currentTrackIndex].transform.position - transform.position) * m_turningPower;
        newForward.Normalize();
        newForward.y = 0.0f;
        transform.forward = newForward;
        Debug.DrawLine(transform.position, transform.position + transform.forward  * 2.0f);
    }
    void Move()
    {
        m_currentVelocity += transform.forward * m_acceleration * Time.fixedDeltaTime;
        //Cap speed to top speed
        if (m_currentVelocity.magnitude > m_topSpeed)
            m_currentVelocity = m_currentVelocity.normalized * m_topSpeed;
        transform.position += m_currentVelocity * Time.fixedDeltaTime;
    }
}
