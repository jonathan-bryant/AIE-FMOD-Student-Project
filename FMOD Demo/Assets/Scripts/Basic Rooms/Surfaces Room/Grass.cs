using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

    public ParticleSystem m_grassParticles;
    //rotate grass
    //Random platform animation
    bool m_standing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision a_col)
    {
        if(a_col.gameObject.name.Contains("End Wall"))
        {
            m_grassParticles.Play();
        }
    }
    void OnCollisionExit(Collision a_col)
    {
        if (a_col.gameObject.name.Contains("End Wall"))
        {
            m_grassParticles.Stop();
        }
    }
}
