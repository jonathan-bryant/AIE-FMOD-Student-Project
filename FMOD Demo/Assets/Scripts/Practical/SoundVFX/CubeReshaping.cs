/*=================================================================
Project:		#AIE FMOD#
Developer:		#Cameron Baron#
Company:		#COMPANY#
Date:			#01/08/2016#
==================================================================*/

using UnityEngine;


public class CubeReshaping : MonoBehaviour 
{
    // Public Vars

    // Private Vars
    public Material m_material;
    float pos;

    void Start ()
    {
        m_material = GetComponent<Renderer>().material;

    }
	
	void Update ()
    {
        m_material.SetFloat("Height Adjustment", transform.position.x);

        //float height = Mathf.Sin(-Time.time + transform.position.x * transform.position.z) * 10 + 15;
        //pos = Mathf.Lerp(pos, height, 0.8f);
        //m_material.SetFloat("_worldX", height);
        //pos = height;
    }

	#region Private Functions

	#endregion
}
