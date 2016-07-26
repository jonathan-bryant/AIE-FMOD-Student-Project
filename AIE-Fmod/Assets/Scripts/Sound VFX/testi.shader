using UnityEngine;
using System.Collections;

public class VertexMod2 : MonoBehaviour {


	//  private var mesh:Mesh;
	Vector3[] startvertpos;
	Mesh mesh;


	// Use this for initialization
	void Start() {
		mesh = GetComponent<MeshFilter>().mesh;
		startvertpos = mesh.vertices;

	}

	// Update is called once per frame
	void Update() {

		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		int i = 0;
		while (i < vertices.Length) {

			Vector3 localnormal = transform.TransformPoint(normals[i]);
			Vector3 localvertice = transform.TransformPoint(vertices[i]);
			float xn = localvertice[0];
			float zn = localvertice[2];
			localvertice[0] = Mathf.Cos(zn * 6 + Time.time * 20) / 10;
			localvertice[2] = Mathf.Cos(xn * 6 + Time.time * 20) / 10;
			vertices[i][0] = startvertpos[i][0] + localvertice[0];
			vertices[i][2] = startvertpos[i][2] + localvertice[2];
			i++;
		}
		mesh.vertices = vertices;
	}
}