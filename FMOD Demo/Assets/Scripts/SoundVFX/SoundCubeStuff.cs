/*=================================================================
Project:		#PROJECTNAME#
Developer:		#DEVOLPERNAME#
Company:		#COMPANY#
Date:			#CREATIONDATE#
==================================================================*/

using UnityEngine;
using System.Collections.Generic;

public class SoundCubeStuff : MonoBehaviour 
{
    // Public Vars

    // Private Vars
    [SerializeField]    GameObject m_cubePrefab;
    GameObject[][] m_cubes;


	void Start () 
	{
        m_cubes = new GameObject[10][];

        for (int i = 0; i < 10; i++)
        {
            m_cubes[i] = new GameObject[10];
            for (int j = 0; j < 10; j++)
            {
                Vector3 pos = transform.position + new Vector3(15 - (i * 4), 0, 15 - (j * 4));
                m_cubes[i][j] = Instantiate(m_cubePrefab, pos, Quaternion.identity) as GameObject;
                m_cubes[i][j].transform.SetParent(transform);
            }
        }
    }
	
	void Update () 
	{
	
	}

	#region Private Functions

	#endregion
}
