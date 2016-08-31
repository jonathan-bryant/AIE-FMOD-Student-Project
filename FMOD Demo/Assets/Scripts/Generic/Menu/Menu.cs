using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool m_menuIsOpen;
    Animator m_animator;
    bool m_optionsOpen, m_practicalOpen, m_basicOpen;
    void Start()
    {
        m_menuIsOpen = false;
        m_animator = GetComponent<Animator>();
        m_optionsOpen = false;
        m_practicalOpen = false;
        m_optionsOpen = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            
        }
    }

    public void OptionsClick()
    {
        if(!m_optionsOpen)
            m_animator.Play("Options Open");
        else
            m_animator.Play("Options Close");
        m_optionsOpen = !m_optionsOpen;
    }
    public void PracticalClick()
    {
        if (!m_practicalOpen)
            m_animator.Play("Practical Room Open");
        else
            m_animator.Play("Practical Room Close");
        m_practicalOpen = !m_practicalOpen;
    }
    public void BasicClick()
    {
        if (!m_basicOpen)
            m_animator.Play("Basic Room Open");
        else
            m_animator.Play("Basic Room Close");
        m_basicOpen = !m_basicOpen;
    }
    public void QuitClick()
    {

    }
    public void LogoClick()
    {

    }
}