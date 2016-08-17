/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			16/08/2016
==================================================================*/

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    DescriptionCanvasScript scriptRef;

	void Start () 
	{
        scriptRef = GetComponentInParent<DescriptionCanvasScript>();
	}

    #region Private Functions

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scriptRef.m_canvas.alpha > 0)
            scriptRef.FadeIn();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        scriptRef.FadeOut();
    }

    #endregion
}
