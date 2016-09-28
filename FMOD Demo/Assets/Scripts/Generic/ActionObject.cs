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
    public int m_materialIndex = 0;

    public float m_glowSpeed = 4.0f;
    [Range(0.0f, 1.0f)]
    public float m_glowStrength = 0.5f;

    public float m_hoverSpeed = 8.0f;
    [Range(0.0f, 1.0f)]
    public float m_hoverStrength = 0.5f;

    public float m_clickSpeed = 0.5f;
    [Range(0.0f, 1.0f)]
    public float m_clickStrength = 1.0f;

    Material[] m_materials;
    Color m_baseColor;
    public Color m_newColor;
    int m_inQuestion;

    public Vector3 m_localClickDirection;
    Vector3 m_originalLocalPosition;
    Vector3 m_clickLocalPosition;

    public int InQuestion
    {
        get
        {
            return m_inQuestion;
        }
        set
        {
            m_clickElapsed = 0.0f;
            m_inQuestion = value;
        }
    }
    float m_clickElapsed;

    void Start()
    {
        InitGlow();
    }
    void Update()
    {
        UpdateGlow();
    }
    protected void InitGlow()
    {
        m_originalLocalPosition = transform.localPosition;
        m_clickLocalPosition = m_originalLocalPosition + m_localClickDirection;
        if (!m_renderer)
            return;
        m_clickElapsed = 0.0f;
        m_inQuestion = 0;
        m_baseColor = m_renderer.materials[m_materialIndex].GetColor("_EmissionColor");
    }
    protected void UpdateGlow()
    {
        if (!m_renderer)
            return;

        if (m_inQuestion == 0)
        {
            Color col = m_renderer.materials[m_materialIndex].GetColor("_EmissionColor");
            if (m_glowSpeed != 0.0f)
            {
                col = Color.Lerp(m_baseColor, m_newColor, Mathf.Sin(Time.time * m_glowSpeed) * (m_glowStrength * 0.5f) + (m_glowStrength * 0.5f));
            }
            m_renderer.materials[m_materialIndex].SetColor("_EmissionColor", col);
        }
        else if (m_inQuestion == 1)
        {
            Color col = m_renderer.materials[m_materialIndex].GetColor("_EmissionColor");
            if (m_hoverSpeed != 0.0f)
            {
                col = Color.Lerp(m_baseColor, m_newColor, Mathf.Sin(Time.time * m_hoverSpeed) * (m_hoverStrength * 0.5f) + (m_hoverStrength * 0.5f));
            }
            m_renderer.materials[m_materialIndex].SetColor("_EmissionColor", col);
        }
        else if(m_inQuestion == 2)
        {
            m_clickElapsed += Time.deltaTime;
            float clickValue = (1.0f - (m_clickElapsed / m_clickSpeed));
            Color col = Color.Lerp(m_baseColor, m_newColor, clickValue * m_clickStrength);
            m_renderer.materials[m_materialIndex].SetColor("_EmissionColor", col);

            if (transform.position != m_clickLocalPosition)
            {
                float lerpValue = Mathf.Sin((Mathf.PI) * clickValue);
                transform.localPosition = Vector3.Lerp(m_originalLocalPosition, m_clickLocalPosition, lerpValue);
            }

            if (m_clickElapsed > m_clickSpeed)
            {
                m_clickElapsed = 0.0f;
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
