/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class drawcursor : MonoBehaviour 
{
    // Public Vars
    public Image image;
	// Private Vars

	void Start () 
	{
        image = GetComponent<Image>();
	}
	
	void Update ()
    {
        image.rectTransform.localPosition = Input.mousePosition;
    }

	#region Private Functions

	#endregion
}
