using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleTargetEmitter : BaseTarget
{
    public Target m_target;
    public int m_numOfTargets;
    List<Target> m_targets;

    public float m_distanceFromCenter;
    
    public float m_targetSpeed;

	void Start ()
    {
        m_targets = new List<Target>();
        for(int i = 0; i < m_numOfTargets; ++i)
        {
            Target t = Instantiate(m_target);
            t.transform.position = transform.position + (transform.up * m_distanceFromCenter);
            float angle = Mathf.Rad2Deg * Mathf.PI * 2.0f * (i / (float)m_numOfTargets);
            t.transform.RotateAround(transform.position, transform.right, angle);
            t.m_Parent = this;
            m_targets.Add(t);
        }
        Debug.DrawRay(transform.position, transform.forward, Color.red);
	}
	
	void Update ()
    {
        MoveTargets();
	}

    void MoveTargets()
    {
        for(int i = 0; i < m_targets.Count; ++i)
        {
            m_targets[i].transform.RotateAround(transform.position, transform.right, 90.0f * Time.deltaTime * m_targetSpeed);
        }
    }
    public override void Hit(Target a_target)
    {
        Destroy(a_target.gameObject);
        m_targets.Remove(a_target);
    }
}