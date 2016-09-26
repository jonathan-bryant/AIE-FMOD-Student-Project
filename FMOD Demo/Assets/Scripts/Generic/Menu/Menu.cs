/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                The main dropdown menu.                                         |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool m_menuIsOpen;
    Animator m_animator;
    bool m_optionsOpen, m_practicalOpen, m_basicOpen;
    ActorControls m_actor;
    bool m_alreadyDisabled;

    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
        m_menuIsOpen = false;
        m_animator = GetComponent<Animator>();
        m_optionsOpen = false;
        m_practicalOpen = false;
        m_optionsOpen = false;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!m_menuIsOpen)
            {
                m_menuIsOpen = true;
                m_animator.Play("Pause Open");
                m_alreadyDisabled = m_actor.DisableMouse;
                if(!m_alreadyDisabled)
                    m_actor.DisableMouse = true;
            }
            else
            {
                CloseMenu();
            }
        }
#elif UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_menuIsOpen = !m_menuIsOpen;
            if (m_menuIsOpen)
            {
                m_animator.Play("Pause Open");
                m_actor.DisableMovementAndMouse(true);
            }
            else
            {
                m_animator.Play("Pause Close");
                m_actor.DisableMovementAndMouse(false);
                if (m_optionsOpen)
                {
                    m_animator.Play("Options Close");
                    m_optionsOpen = false;
                }
                if (m_practicalOpen)
                {
                    m_animator.Play("Practical Room Close");
                    m_practicalOpen = false;
                }
                if (m_basicOpen)
                {
                    m_animator.Play("Basic Room Close");
                    m_basicOpen = false;
                }
            }
        }
#endif

    }

    public void OptionsClick()
    {
        if (m_menuIsOpen)
        {
            if (!m_optionsOpen)
                m_animator.Play("Options Open");
            else
                m_animator.Play("Options Close");
            m_optionsOpen = !m_optionsOpen;
            if (m_practicalOpen)
            {
                m_animator.Play("Practical Room Close");
                m_practicalOpen = false;
            }
            if (m_basicOpen)
            {
                m_animator.Play("Basic Room Close");
                m_basicOpen = false;
            }
        }
    }
    public void PracticalClick()
    {
        if (m_menuIsOpen)
        {
            if (!m_practicalOpen)
                m_animator.Play("Practical Room Open");
            else
                m_animator.Play("Practical Room Close");
            m_practicalOpen = !m_practicalOpen;
        }
        if (m_basicOpen)
        {
            m_animator.Play("Basic Room Close");
            m_basicOpen = false;
        }
        if (m_optionsOpen)
        {
            m_animator.Play("Options Close");
            m_optionsOpen = false;
        }
    }
    public void BasicClick()
    {
        if (m_menuIsOpen)
        {
            if (!m_basicOpen)
                m_animator.Play("Basic Room Open");
            else
                m_animator.Play("Basic Room Close");
            m_basicOpen = !m_basicOpen;
        }
        if (m_optionsOpen)
        {
            m_animator.Play("Options Close");
            m_optionsOpen = false;
        }

        if (m_practicalOpen)
        {
            m_animator.Play("Practical Room Close");
            m_practicalOpen = false;
        }
    }
    public void QuitClick()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        
    }
    public void LogoClick()
    {
        Application.OpenURL("http://http://www.fmod.org/");
    }

    public void CloseMenu()
    {
        m_menuIsOpen = false;
        m_animator.Play("Pause Close");
        if (!m_alreadyDisabled)
            m_actor.DisableMouse = false;
        if (m_optionsOpen)
        {
            m_animator.Play("Options Close");
            m_optionsOpen = false;
        }
        if (m_practicalOpen)
        {
            m_animator.Play("Practical Room Close");
            m_practicalOpen = false;
        }
        if (m_basicOpen)
        {
            m_animator.Play("Basic Room Close");
            m_basicOpen = false;
        }
    }

    public void TeleportToRoom(GameObject a_door)
    {
        if(a_door)
        {
            CloseMenu();
            Vector3 pos = m_actor.transform.position;
            pos = a_door.transform.position + (a_door.transform.right * 2.0f);
            m_actor.transform.position = pos;

            m_actor.transform.LookAt(new Vector3(a_door.transform.position.x, 0.0f, a_door.transform.position.z));
            m_actor.transform.localEulerAngles = new Vector3(0.0f,m_actor.transform.localEulerAngles.y, 0.0f);
            Camera.main.transform.localEulerAngles = Vector3.zero;
            Camera.main.transform.Rotate(Vector3.right * -10.0f);
        }
    }
}