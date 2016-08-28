using UnityEngine;
using System.Collections;

public class CubeAnticulling : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.bounds = new Bounds(transform.position, new Vector3(100, 100, 100));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
