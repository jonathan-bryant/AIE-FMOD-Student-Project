/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron  
Company:		FMOD
Date:			01/08/2016
==================================================================*/

using UnityEngine;

public class SoundCubeStuff : MonoBehaviour 
{
    // Public Vars
    public int m_gridWidth = 15;                        // Customisable grid size.

    // Private Vars
    [SerializeField]    GameObject m_cubePrefab;        // Prefab to use for visualisation.
    GameObject[][] m_cubes;                             // Array (grid) of prefabs.
    MainSound m_soundRef;                               // Reference to main sound script.
    float[] m_soundFFTData;                             // Array for sound fft data to be stored in.

	void Start () 
	{
        m_soundRef = FindObjectOfType<MainSound>();     // The first object found with the wanted script attached.
        
        if (m_soundRef == null)
        {
            Debug.LogError("No 'Main Sound' Script in scene!!!");
            DestroyImmediate(this);
            return;
        }

        // Create grid of cubes using an Array of Array's
        m_cubes = new GameObject[m_gridWidth][];

        for (int i = 0; i < m_gridWidth; i++)
        {
            m_cubes[i] = new GameObject[m_gridWidth];
            for (int j = 0; j < m_gridWidth; j++)
            {
                Vector3 pos = transform.position + new Vector3(2.5f - (i * 0.6f), 0, 2.5f - (j * 0.6f));
                m_cubes[i][j] = Instantiate(m_cubePrefab, pos, Quaternion.identity) as GameObject;
                m_cubes[i][j].transform.SetParent(transform);
            }
        }		
    }
	
	void Update () 
	{        
        // Get the update spectrum data from the main sound script;
        m_soundFFTData = m_soundRef.m_fftArray;
        // Loop through all cubes one at a time.
		for (int row = 0; row < m_gridWidth; row++)
        {
            for (int col = 0; col < m_gridWidth; col++)
            {
				int newCol = col;
				if (col < 14)
				{
					 newCol += 1;
				}

                // Assign the cube 
                m_cubes[row][col].GetComponent<Renderer>().material.SetFloat("_Amount", m_soundFFTData[(row + 1) * (newCol)]);
            }
        }
    }

	#region Private Functions

	#endregion
}
