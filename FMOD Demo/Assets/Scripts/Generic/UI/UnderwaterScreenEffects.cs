using UnityEngine;


public class UnderwaterScreenEffects : MonoBehaviour 
{
    // Public Vars
    public GameObject m_underwaterUI;

	// Private Vars

	void Start () 
	{
	    if (m_underwaterUI)
        {
            m_underwaterUI.SetActive(false);
        }
	}

	#region Private Functions

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Water"))
        {
            m_underwaterUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Water"))
        {
            m_underwaterUI.SetActive(false);
        }
    }

    #endregion
}
