using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{
    public Orchestrion m_orchestrion;
	void Start()
    {
	
	}
	void Update()
    {

    }

    void OnTriggerEnter(Collider a_col)
    {
        Debug.Log("Collision");
        m_orchestrion.Play();
    }
}