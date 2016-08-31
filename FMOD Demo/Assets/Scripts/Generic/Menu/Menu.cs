using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool m_isPaused;
    enum Selection
    {
        Pause,
        Practical,
        Basic,
        Option
    }
    Selection m_currentSelection;
    Animator m_animator;
    bool m_finishedAnimation;
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_finishedAnimation = true;
    }

    void Update()
    {
        if (m_finishedAnimation)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_isPaused = !m_isPaused;
                m_animator.Play("Pause Open", 0, m_isPaused ? 0 : 1);
                m_currentSelection = Selection.Pause;
            }
            if (m_isPaused)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (m_currentSelection == Selection.Options)
                        m_animator.Play("Options Close");
                    m_animator.Play("Options Open");
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    m_animator.Play("Basic Room Open");
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    m_animator.Play("Practical Room Open");
                }
            }
        }
    }
    public void Stop()
    {
        m_animator.SetFloat("Speed", 0.0f);
    }
}