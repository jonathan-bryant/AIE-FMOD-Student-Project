using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool m_isPaused;
    Animator m_animator;
	void Start () {
        m_animator = GetComponent<Animator>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_isPaused = !m_isPaused;
            m_animator.Play(1);
        }
	}
}
