using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cart : ActionObject
{
    public Track m_trackHolder;
    Vector3 m_newHeading;
    public float m_topSpeed = 3.0f;
    public float m_acceleration = 30.0f;
    public float m_turningPower = 0.4f;
    Vector3 m_currentVelocity;
    List<Transform> m_tracks;
    int m_currentTrackIndex;
    public Transform m_seat;
    public float m_threshold = 1.0f;
    bool m_active;
    public int m_Rounds;
    int m_currentRound;
    public int CurrentRound { get { return m_currentRound; } }
    public ScoreBoard m_scoreBoard;

    public BaseTarget[] m_targets;

    void Start()
    {
        m_currentRound = 1;
        m_active = false;
        m_tracks = new List<Transform>();
        //Add children from track holder to m_tracks(list of transforms) 
        for (int i = 0; i < m_trackHolder.transform.childCount; ++i)
        {
            m_tracks.Add(m_trackHolder.transform.GetChild(i));
        }
        m_currentTrackIndex = m_trackHolder.m_startingIndex;
        //set the position of cart to first tracks position but if track holder is not 3D ignore the y axis.
        if (!m_trackHolder.m_3D)
            transform.position = new Vector3(m_tracks[m_currentTrackIndex].transform.position.x, transform.position.y, m_tracks[m_currentTrackIndex].transform.position.z);
        else
            transform.position = m_tracks[m_currentTrackIndex].transform.position;

        //Set the carts forward to the next track in the list.
        transform.forward = m_tracks[m_currentTrackIndex + 1].transform.position - transform.position;
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        if (m_active)
        {
            if (!Finish())
            {
                CalculateHeading();
                Move();
            }
        }
    }

    public override void Use(bool a_use)
    {
        if (a_use)
        {
            //if the cart is not active set it to active
            if (!m_active)
            {
                m_active = true;
                //if the current track index is equal to the starting index increment it because nothing else will while the cart is finished
                if (m_currentTrackIndex == m_trackHolder.m_startingIndex)
                {
                    if(m_currentRound != m_Rounds)
                        m_currentTrackIndex++;
                    Vector3 startPos = m_tracks[m_trackHolder.m_startingIndex].transform.position;
                    if (!m_trackHolder.m_3D)
                    {
                        if (m_currentRound == 1 && transform.position == new Vector3(startPos.x, transform.position.y, startPos.z))
                        {
                            m_scoreBoard.ClearScore();
                            foreach (BaseTarget t in m_targets)
                            {
                                t.Reset();
                            }
                        }
                    }
                    else
                    {
                        if (m_currentRound == 1 && transform.position == startPos)
                        {
                            m_scoreBoard.ClearScore();
                            foreach (BaseTarget t in m_targets)
                            {
                                t.Reset();
                            }
                        }
                    }
                }
                foreach (BaseTarget t in m_targets)
                {
                    t.Play();
                }
                return;
            }
        }
        foreach (BaseTarget t in m_targets)
        {
            t.Stop();
        }
        m_active = false;
    }

    bool Finish()
    {
        //if the cart is at the starting point
        if (m_currentTrackIndex == m_trackHolder.m_startingIndex)
        {

            //if the track is not a 3d one leave the y axis to be the carts y axis not the current tracks
            if (!m_trackHolder.m_3D)
            {
                if ((new Vector3(m_tracks[m_trackHolder.m_startingIndex].transform.position.x, transform.position.y, m_tracks[m_trackHolder.m_startingIndex].transform.position.z) - transform.position).magnitude <= m_threshold)
                {
                    m_currentRound++;
                    if (m_currentRound <= m_Rounds)
                    {
                        m_currentTrackIndex++;
                        return false;
                    }

                    transform.position = new Vector3(m_tracks[m_trackHolder.m_startingIndex].transform.position.x, transform.position.y, m_tracks[m_trackHolder.m_startingIndex].transform.position.z);
                    Vector3 nextTrack = m_tracks[(m_trackHolder.m_startingIndex + 1 % m_tracks.Count)].transform.position;
                    transform.forward = (new Vector3(nextTrack.x, 0.0f, nextTrack.z) - transform.position).normalized;
                    //set the cart to be disabled
                    m_active = false;
                    //remove it's velocity
                    m_currentVelocity = Vector3.zero;
                    m_currentRound = 1;
                    foreach (BaseTarget t in m_targets)
                    {
                        t.Stop();
                    }
                    return true;
                }
            }
            else
            {
                if ((m_tracks[m_trackHolder.m_startingIndex].transform.position - transform.position).magnitude <= m_threshold)
                {
                    m_currentRound++;
                    if (m_currentRound <= m_Rounds)
                    {
                        m_currentTrackIndex++;
                        return false;
                    }

                    transform.position = m_tracks[m_trackHolder.m_startingIndex].transform.position;
                    transform.forward = (m_tracks[(m_trackHolder.m_startingIndex + 1 % m_tracks.Count)].transform.position - transform.position).normalized;
                    m_active = false;
                    m_currentVelocity = Vector3.zero;
                    m_currentRound = 1;
                    foreach (BaseTarget t in m_targets)
                    {
                        t.Stop();
                    }
                    return true;
                }
            }
        }
        return false;
    }
    void CalculateHeading()
    {
        //If track is not 3D check distances using the carts y axis instead of the tracks y axis.
        if (!m_trackHolder.m_3D)
        {
            //if the current target track has been reached within the threshold. If it has then increment it or set it back to 0 if last track in array has been reached.
            if ((new Vector3(m_tracks[m_currentTrackIndex].transform.position.x, transform.position.y, m_tracks[m_currentTrackIndex].transform.position.z) - transform.position).magnitude <= m_threshold)
            {
                ++m_currentTrackIndex;
                if (m_currentTrackIndex >= m_trackHolder.transform.childCount)
                    m_currentTrackIndex = 0;
            }
        }
        else
        {
            if ((m_tracks[m_currentTrackIndex].transform.position - transform.position).magnitude <= m_threshold)
            {
                ++m_currentTrackIndex;
                if (m_currentTrackIndex >= m_trackHolder.transform.childCount)
                    m_currentTrackIndex = 0;
            }
        }

        //Turn towards next track
        Vector3 newForward = transform.forward + (m_tracks[m_currentTrackIndex].transform.position - transform.position) * m_turningPower;
        newForward.Normalize();
        //If the track is not 3D then set the y direction to 0
        if (!m_trackHolder.m_3D)
            newForward.y = 0.0f;
        transform.forward = newForward;
        Debug.DrawLine(transform.position, transform.position + transform.forward * 2.0f);
    }
    void Move()
    {
        //Add to velocity with acceleration
        m_currentVelocity += transform.forward * m_acceleration * Time.fixedDeltaTime;
        //Cap speed to top speed
        if (m_currentVelocity.magnitude > m_topSpeed)
            m_currentVelocity = m_currentVelocity.normalized * m_topSpeed;
        //Add to position with velocity
        transform.position += m_currentVelocity * Time.fixedDeltaTime;
    }
}