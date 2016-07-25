using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
    public float m_minRadius;
    public float m_maxRadius;
    public BaseTarget m_Parent;

    void Start () {
        float rngSize = Random.Range(m_minRadius, m_maxRadius);
        transform.localScale = new Vector3(rngSize, transform.localScale.y, rngSize);
	}
	
	void Update () {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
	}

    void OnCollisionEnter(Collision a_col)
    {
        if (a_col.gameObject.name.Contains("Bullet"))
        {
            if (m_Parent)
                m_Parent.Hit(this);
        }
    }
}
