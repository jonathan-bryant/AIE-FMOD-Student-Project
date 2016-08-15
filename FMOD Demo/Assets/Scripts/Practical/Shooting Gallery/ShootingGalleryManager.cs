using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShootingGalleryManager : MonoBehaviour
{
    public Cart m_cart;
    public int m_rounds;
    public int m_currentRound;

    public int m_maxScore;
    int m_currentScore;

    public Track m_trackHolder;
    List<Transform> m_tracks;
    int m_currentTrackIndex;
    public int CurrentTrackIndex { get { return m_currentTrackIndex; } }
    public int StartingTrackIndex { get { return m_trackHolder.m_startingIndex; } }
    public int TrackCount { get { return m_tracks.Count; } }

    public SG_MainSpeaker m_mainSpeaker;
    public BaseTarget[] m_targets;
    public Text[] m_scores;

    bool m_active;
    bool m_finished;
    public bool IsActive { get { return m_active; } }

    void Start()
    {
        m_finished = false;
        m_currentScore = 0;
        m_currentRound = 0;
        m_active = false;

        //store tracks
        m_tracks = new List<Transform>();
        for (int i = 0; i < m_trackHolder.transform.childCount; ++i)
        {
            m_tracks.Add(m_trackHolder.transform.GetChild(i));
        }
        m_currentTrackIndex = m_trackHolder.m_startingIndex;

        //set carts position and forward
        m_cart.transform.position = m_tracks[m_currentTrackIndex].transform.position;
        m_cart.transform.forward = m_tracks[++m_currentTrackIndex].transform.position - m_cart.transform.position;
    }
    void Update()
    {

    }

    public void Play()
    {
        if (m_currentRound >= m_rounds)
            Reset();
        m_active = true;

        m_mainSpeaker.IsActive(true);
        m_mainSpeaker.Pause(false);
        if (m_currentRound == m_rounds - 1)
        {
            m_mainSpeaker.SetRound(2);
        }
        else if (m_currentRound > 0 || m_rounds == 2)
        {
            m_mainSpeaker.SetRound(1);
        }


        foreach (BaseTarget t in m_targets)
        {
            t.Play();
        }
    }
    public void Pause()
    {
        m_active = false;
        if (!m_finished)
            m_mainSpeaker.Pause(true);
        m_mainSpeaker.IsActive(false);
        foreach (BaseTarget t in m_targets)
        {
            t.Stop();
        }
    }
    void Reset()
    {
        m_finished = false;
        //Set round
        m_currentRound = 0;
        //Clear score
        ClearScore();

        ResetTargets();

        //Reset game result
        m_mainSpeaker.SetGameResult(0);
        //Reset round
        m_mainSpeaker.SetRound(0);
    }
    void ResetTargets()
    {
        //Reset targets
        foreach (BaseTarget t in m_targets)
        {
            t.Reset();
        }
    }
    void ResetCart()
    {
        //Set position
        m_cart.transform.position = m_tracks[m_currentTrackIndex - 1].transform.position;
        //Set forward
        m_cart.transform.forward = m_tracks[m_currentTrackIndex].transform.position - m_cart.transform.position;
        //Zero out currentVelocity
        m_cart.CurrentVelocity = Vector3.zero;
    }

    public void AddScore(int a_points)
    {
        m_currentScore += a_points;
        foreach (Text t in m_scores)
        {
            t.text = m_currentScore.ToString();
        }
    }
    public void ClearScore()
    {
        m_currentScore = 0;
        foreach (Text t in m_scores)
        {
            t.text = m_currentScore.ToString();
        }
    }
    public void IncremetTrack()
    {
        m_currentTrackIndex++;
        //Wrap around track index if it gets out of range of the size of tracks count
        m_currentTrackIndex %= (m_tracks.Count - 1);

        if (m_currentTrackIndex - m_trackHolder.m_startingIndex == 1)
        {
            //You have reached the start again increment rounds, if last round then check if you win or not
            m_currentRound++;
            if (m_currentRound >= m_rounds)
            {
                m_mainSpeaker.SetGameResult(m_currentScore >= m_maxScore ? 1 : -1);
                m_finished = true;
                Pause();
                ResetCart();
            }
            else
            {
                ResetTargets();
                if (m_currentRound == m_rounds - 1)
                {
                    m_mainSpeaker.SetRound(2);
                }
                else if (m_currentRound > 0)
                {
                    m_mainSpeaker.SetRound(1);
                }
            }
        }
    }
    public Transform GetCurrentTrack()
    {
        return m_tracks[m_currentTrackIndex];
    }
    public Transform GetNextTrack()
    {
        return m_tracks[m_currentTrackIndex + 1];
    }
}
