using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionObject : MonoBehaviour
{
    bool m_InUse = false;
    bool m_isHighlighted;
    protected string m_description;
    protected bool m_descriptionIs3D;
    protected Vector3 m_descriptionPosition;
    
    void Start()
    {

    }
    void Update()
    {
        if (m_isHighlighted)
            DisplayDescription();
    }

    public void Use(GameObject sender)
    {
        m_InUse = !m_InUse;
        Action(sender, m_InUse);
    }
    protected virtual void Action(GameObject sender, bool a_use)
    {

    }

    void DisplayDescription()
    {
        //Show description
        if(m_descriptionIs3D)
        {

        }
        else
        {

        }
    }
}
