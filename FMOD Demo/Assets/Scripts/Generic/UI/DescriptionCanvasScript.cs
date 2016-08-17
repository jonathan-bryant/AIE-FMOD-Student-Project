/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			16/08/2016
==================================================================*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DescriptionCanvasScript : MonoBehaviour
{
	public bool m_active = false;
	public float m_timeUntilFade = 2.0f;
    public bool m_billboard = true;
    [Range(0, 1)]    public float m_minAlpha = 0.0f;

	public CanvasGroup m_canvas;
    List<Image> images;
	void Start ()
	{
		m_canvas = GetComponentInChildren<CanvasGroup>();
        m_canvas.alpha = m_minAlpha;
    }
	

	void Update ()
    {
        // Make canvas billboard to camera.
        if (m_billboard)
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Vector3.up);

        if (!m_active && m_canvas.alpha > m_minAlpha)
		{
			m_canvas.alpha -= Time.deltaTime / 2.0f;
		}
		else if (m_active && m_canvas.alpha < 1)
		{
			m_canvas.alpha += Time.deltaTime / 2.0f;
        }
        
	}

	public void FadeIn()
    {
        m_active = true;
    }

	public void FadeOut()
	{
		//StartCoroutine(WaitThenFadeOut());
        m_active = false;
    }

	IEnumerator WaitThenFadeOut()
	{
		yield return new WaitForSeconds(m_timeUntilFade);
		m_active = false;
	}
}
