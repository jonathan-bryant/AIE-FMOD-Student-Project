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

[ExecuteInEditMode]
public class DescriptionCanvasScript : MonoBehaviour
{
	public bool m_active = false;
	public float m_timeUntilFade = 2.0f;
    float m_fadeCounter = 0.0f;
    public bool m_billboard = true;
    [Range(0, 1)]    public float m_minAlpha = 0.0f;

	public CanvasGroup m_canvas;
    List<Image> images;

    Transform m_playerTransform;

	void Start ()
	{
		m_canvas = GetComponentInChildren<CanvasGroup>();
        m_canvas.alpha = m_minAlpha;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	

	void Update ()
    {
        // Make canvas billboard to camera.
        if (m_billboard)
        {
            transform.LookAt(m_playerTransform.position, Vector3.up);
            transform.Rotate(Vector3.up, 180.0f);
        }

        if (!m_active && m_fadeCounter < m_timeUntilFade)
		{
            m_fadeCounter += Time.deltaTime;
		}
		else if (m_active && m_canvas.alpha < 1)
		{
			m_canvas.alpha += Time.deltaTime / 2.0f;
            m_fadeCounter = 0.0f;
        }

        if (m_fadeCounter >= m_timeUntilFade && m_canvas.alpha > m_minAlpha)
        {
            m_canvas.alpha -= Time.deltaTime / 2.0f;
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
