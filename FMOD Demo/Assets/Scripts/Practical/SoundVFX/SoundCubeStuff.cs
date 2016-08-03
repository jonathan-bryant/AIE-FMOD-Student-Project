/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron  
Company:		FMOD
Date:			01/08/2016
==================================================================*/

using System;
using UnityEngine;
using System.Collections.Generic;

public class SoundCubeStuff : MonoBehaviour 
{
    // Public Vars
    public int m_gridWidth = 15;
    public MainSound m_soundRef;

    // Private Vars
    [SerializeField]    GameObject m_cubePrefab;        // Prefab to use for visualisation.
    GameObject[][] m_cubes;                             // Array (grid) of prefabs.
    float[] m_soundFFTData;


	void Start () 
	{
        m_cubes = new GameObject[m_gridWidth][];

        for (int i = 0; i < m_gridWidth; i++)
        {
            m_cubes[i] = new GameObject[m_gridWidth];
            for (int j = 0; j < m_gridWidth; j++)
            {
                Vector3 pos = transform.position + new Vector3(20 - (i * 3), 0, 20 - (j * 3));
                m_cubes[i][j] = Instantiate(m_cubePrefab, pos, Quaternion.identity) as GameObject;
                m_cubes[i][j].transform.SetParent(transform);
            }
        }
    }
	
	void Update () 
	{
        m_soundFFTData = m_soundRef.m_fftArray;

        for (int i = 1; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                m_cubes[i][j].GetComponent<CubeReshaping>().m_material.SetFloat("_Amount", m_soundFFTData[i * j]);
            }
        }
    }

	#region Private Functions

	#endregion
}
