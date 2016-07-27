using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cart : ActionObject
{
    public float m_topSpeed = 2.0f;
    public float m_acceleration = 10.0f;
    public float m_turningPower = 0.1f;
    public float m_threshold = 0.5f;
    Vector3 m_currentVelocity;

    public Transform m_seat;

    public Track m_trackHolder;
    List<Transform> m_tracks;
    int m_currentTrackIndex;

    bool m_active;
    public int m_Rounds;
    public int m_currentRound;
    public int CurrentRound { get { return m_currentRound; } }

    public SG_MainSpeaker m_mainSpeaker;
    public ScoreBoard m_scoreBoard;
    public BaseTarget[] m_targets;

    void Start()
    {
        m_currentRound = 0;
        m_active = false;
        m_tracks = new List<Transform>();
        for (int i = 0; i < m_trackHolder.transform.childCount; ++i)
        {
            m_tracks.Add(m_trackHolder.transform.GetChild(i));
        }
        m_currentTrackIndex = m_trackHolder.m_startingIndex;

        if (!m_trackHolder.m_3D)
            transform.position = new Vector3(m_tracks[m_currentTrackIndex].transform.position.x, transform.position.y, m_tracks[m_currentTrackIndex].transform.position.z);
        else
            transform.position = m_tracks[m_currentTrackIndex].transform.position;
        transform.forward = m_tracks[m_currentTrackIndex + 1].transform.position - transform.position;
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        if (m_active)
        {
            CalculateHeading();
            Move();
            if(AtStart())
            {
                ResetCart();
                Pause();
            }
        }
    }

    public override void Use(bool a_use)
    {
        if (a_use)
        {
            //If player is at starting point restart otherwise play
            if (AtStart())
            {
                Restart();
            }
            Play();
        }
        else
        {
            Pause();
        }
    }

    bool AtStart()
    {
        //return true if currentRound is first round and track index is starting index.
        Vector3 trackPosition = new Vector3(m_tracks[m_trackHolder.m_startingIndex].transform.position.x, transform.position.y, m_tracks[m_trackHolder.m_startingIndex].transform.position.z);
        return ((m_currentRound == 0 || m_currentRound == m_Rounds) && (m_currentTrackIndex == m_trackHolder.m_startingIndex) && ((trackPosition - transform.position).magnitude <= m_threshold));
    }
    void CalculateHeading()
    {
        bool change = false;
        //If track is not 3D check distances using the carts y axis instead of the tracks y axis.
        if (!m_trackHolder.m_3D)
        {
            //if the current target track has been reached within the threshold. If it has then increment it or set it back to 0 if last track in array has been reached.
            Vector3 trackPosition = new Vector3(m_tracks[m_trackHolder.m_startingIndex].transform.position.x, transform.position.y, m_tracks[m_trackHolder.m_startingIndex].transform.position.z);
            if ((trackPosition - transform.position).magnitude <= m_threshold)
            {
                change = true;
            }
        }
        else
        {
            if ((m_tracks[m_currentTrackIndex].transform.position - transform.position).magnitude <= m_threshold)
            {
                change = true;
            }
        }
        if (change)
        {
            ++m_currentTrackIndex;
            //When at the startpoint increment current round
            if (m_currentTrackIndex == m_tracks.Count - 1)
            {
                m_currentRound++;
                m_mainSpeaker.SetRound(m_currentRound);
            }
            //Wrap around track index if it gets out of range of the size of tracks count
            m_currentTrackIndex %= (m_tracks.Count - 1);
            
        }

        //Turn towards next track
        Vector3 newForward = transform.forward + (m_tracks[m_currentTrackIndex].transform.position - transform.position) * m_turningPower;
        newForward.Normalize();
        //If the track is not 3D then set the y direction to 0
        if (!m_trackHolder.m_3D)
            newForward.y = 0.0f;
        //Set the carts forward to the new forward
        transform.forward = newForward;
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
    void Play()
    {
        m_active = true;
        m_mainSpeaker.IsActive(true);
        foreach (BaseTarget t in m_targets)
        {
            t.Play();
        }
    }
    void Pause()
    {
        m_active = false;
        m_mainSpeaker.IsActive(false);
        foreach (BaseTarget t in m_targets)
        {
            t.Stop();
        }
    }
    void Restart()
    {
        //Set round
        m_currentRound = 0;
        //Clear score
        m_scoreBoard.ClearScore();

        ResetCart();

        //Reset targets
        foreach (BaseTarget t in m_targets)
        {
            t.Reset();
        }
        //Reset game result
        m_mainSpeaker.SetGameResult(0);
        //Reset round
        m_mainSpeaker.SetRound(m_currentRound);
    }
    void ResetCart()
    {
        //Set trackindex to start
        m_currentTrackIndex = m_trackHolder.m_startingIndex;
        //Set position
        if (!m_trackHolder.m_3D)
        {
            transform.position = new Vector3(m_tracks[m_currentTrackIndex].transform.position.x, transform.position.y, m_tracks[m_currentTrackIndex].transform.position.z);
        }
        else
        {
            transform.position = m_tracks[m_currentTrackIndex].transform.position;
        }
        //Set forward
        transform.forward = m_tracks[m_currentTrackIndex + 1].transform.position - transform.position;
        //Zero out currentVelocity
        m_currentVelocity = Vector3.zero;
    }
}