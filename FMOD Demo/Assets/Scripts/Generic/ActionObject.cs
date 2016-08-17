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

    //[SerializeField] GameObject m_descriptionPrefab;
    [SerializeField]
     GameObject m_descriptionObject;
    DescriptionCanvasScript m_descriptionScript;

    void Awake()
    {
        //m_descriptionObject = Instantiate(m_descriptionPrefab, transform.position + new Vector3(0, 1, 4), Quaternion.identity) as GameObject;
        //m_descriptionObject.transform.SetParent(transform);
        m_descriptionScript = m_descriptionObject.GetComponent<DescriptionCanvasScript>();
    }

    public void Use(GameObject sender)
    {
        m_InUse = !m_InUse;
        Action(sender, m_InUse);
    }
    protected virtual void Action(GameObject sender, bool a_use)
    {

    }

    void OnMouseEnter()
    {
        m_descriptionScript.FadeIn();
    }

    void OnMouseExit()
    {
        m_descriptionScript.FadeOut();
    }
    void OnMouseOver()
    {
        m_descriptionScript.m_active = true;
    }
}
