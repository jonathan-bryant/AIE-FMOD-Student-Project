using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public SG_MainSpeaker m_mainSpeaker;
    public int m_winScore;
    int m_score;
    public Text[] m_scores;
    public Cart m_cart;
	// Use this for initialization
	void Start () {
        m_score = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (m_cart.CurrentRound == m_cart.m_Rounds)
        {
            m_mainSpeaker.SetGameResult(m_score >= m_winScore ? 1 : -1);
            m_mainSpeaker.IsActive(false);
        }
    }
    public void AddScore(int a_points)
    {
        m_score += a_points;
        foreach (Text t in m_scores)
        {
            t.text = m_score.ToString();
        }
    }
    public void ClearScore()
    {
        m_score = 0;
        foreach (Text t in m_scores)
        {
            t.text = m_score.ToString();
        }
    }
}
