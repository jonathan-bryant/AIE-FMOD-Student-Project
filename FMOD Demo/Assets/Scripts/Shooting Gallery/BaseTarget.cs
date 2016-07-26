using UnityEngine;
using System.Collections;

public class BaseTarget : MonoBehaviour {

    public int m_points;
    public ScoreBoard m_scoreBoard;
    protected bool m_active;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    public virtual void Hit(Target a_target)
    {

    }
    public virtual void Reset()
    {

    }
    public virtual void Play()
    {
        m_active = true;
    }
    public virtual void Stop()
    {
        m_active = false;
    }
}
