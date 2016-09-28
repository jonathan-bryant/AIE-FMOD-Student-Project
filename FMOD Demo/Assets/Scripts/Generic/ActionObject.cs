/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     No                                                              |
|   Description:                The base class for all objects that can be intacted with.       |
|   Derived classes will inherit the appropriate classes.                                       |
===============================================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionObject : MonoBehaviour
{
    public KeyCode[] m_actionKeys; //List of all keys that can be pressed to activate the ActionObject
    public string[] m_actionVerbs; //List of corresponding keys strings that will be displayed on the UI

    public Renderer m_renderer;
    Material m_original;
    Material m_newMaterial;
    Color m_baseColor;
    public Color m_newColor;
    int m_inQuestion;
    public int InQuestion
    {
        get
        {
            return m_inQuestion;
        }
        set
        {
            m_elapsed = 0.0f;
            m_inQuestion = value;
        }
    }
    float m_elapsed;
    public float m_ClickTimer;

    protected void InitGlow()
    {
        m_elapsed = 0.0f;
        m_inQuestion = 0;
        m_original = m_renderer.material;
        m_newMaterial = new Material(m_original);
        m_renderer.material = m_newMaterial;
        m_newMaterial.EnableKeyword("_EMISSION");
        m_baseColor = m_newMaterial.GetColor("_EmissionColor");
        m_newMaterial.SetColor("_EmissionColor", m_newColor);
    }
    protected void UpdateGlow()
    {
        if (m_inQuestion == 0)
        {
            Color col = Color.Lerp(m_baseColor, m_newColor, Mathf.Sin(Time.time * 4.0f) * (m_newColor.a * 0.5f) + (m_newColor.a * 0.5f));
            m_newMaterial.SetColor("_EmissionColor", col);
        }
        else if (m_inQuestion == 1)
        {
            Color col = Color.Lerp(m_baseColor, m_newColor, Mathf.Sin(Time.time * 8.0f) * (m_newColor.a * 0.5f) + (m_newColor.a * 0.5f));
            m_newMaterial.SetColor("_EmissionColor", col);
        }
        else if(m_inQuestion == 2)
        {
            m_elapsed += Time.deltaTime;
            Color col = Color.Lerp(m_baseColor, m_newColor, 1.0f - (m_elapsed / m_ClickTimer));
            m_newMaterial.SetColor("_EmissionColor", col);
            if(m_elapsed > m_ClickTimer)
            {
                m_elapsed = 0.0f;
                m_inQuestion = 0;
            }
        }
    }
    //When the key has been pressed that frame
    public virtual void ActionPressed(GameObject a_sender, KeyCode a_key)
    {

    }
    //When the key has been released that frame
    public virtual void ActionReleased(GameObject a_sender, KeyCode a_key)
    {

    }
    //When the key has been held down for more than one frame
    public virtual void ActionDown(GameObject a_sender, KeyCode a_key)
    {

    }
}
