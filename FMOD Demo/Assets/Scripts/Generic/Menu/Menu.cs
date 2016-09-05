using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool m_menuIsOpen;
    Animator m_animator;
    bool m_optionsOpen, m_practicalOpen, m_basicOpen;
    ActorControls m_actor;

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
        if (Input.GetKeyDown(KeyCode.P))
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

    public void TeleportToRoom(string a_room)
    {
        GameObject obj = GameObject.Find(a_room);
        if(obj)
        {
            m_actor.transform.position = obj.transform.position + (-obj.transform.right * 10.0f) + (-obj.transform.forward * 1.266646f) + Vector3.up * 0.7f;
            Vector3 doorCenter = obj.transform.position + (-obj.transform.forward * 1.266646f);

            m_actor.transform.LookAt(new Vector3(doorCenter.x, 0.0f, doorCenter.z));
            m_actor.transform.localEulerAngles = new Vector3(0.0f,m_actor.transform.localEulerAngles.y, 0.0f);
            Camera.main.transform.localEulerAngles = Vector3.zero;
        }
    }
}