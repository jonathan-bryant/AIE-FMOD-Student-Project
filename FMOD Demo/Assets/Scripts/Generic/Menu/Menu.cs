using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool m_isPaused;
    int m_option;
    Animator m_animator;
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_isPaused = !m_isPaused;
            m_animator.SetFloat("Speed", m_isPaused ? 1 : -1);
            m_animator.Play("Pause Open");
        }
        if (m_isPaused)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                m_animator.SetFloat("Speed", 1);
                m_animator.Play("Options Open");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                m_animator.SetFloat("Speed", 1);
                m_animator.Play("Basic Room Open");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                m_animator.SetFloat("Speed", 1);
                m_animator.Play("Practical Room Open");
            }
        }
    }
}