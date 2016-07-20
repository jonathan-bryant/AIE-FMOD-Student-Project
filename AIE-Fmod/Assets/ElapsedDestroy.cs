using UnityEngine;
using System.Collections;

public class ElapsedDestroy : MonoBehaviour {

    public float m_timer;
    float m_elapsed;
	// Use this for initialization
	void Start () {
        m_elapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        m_elapsed += Time.deltaTime;
        if(m_elapsed >= m_timer)
        {
            Destroy(gameObject);
        }
	}
}
