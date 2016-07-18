using UnityEngine;
using System.Collections;

public class Occlusion : MonoBehaviour
{
	FMOD.Geometry geometry;
	int numPolygons = 6;
	int numVerts;
	FMOD.VECTOR[][] geomVerts;
	int[] polygons = new int[6];
	Mesh mesh;

	FMOD.RESULT result;

	void Start ()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		numVerts = mesh.vertexCount;

		geomVerts = new FMOD.VECTOR[6][];
		FMODUnity.RuntimeManager.LowlevelSystem.createGeometry(numPolygons, numVerts, out geometry);

		// Geometry polygons have to be on the same plane, or things will break
		for (int i = 0; i < 6; ++i)
		{
			FMOD.VECTOR[] temp = new FMOD.VECTOR[4];
			for (int j = 0; j < 4; ++j)
			{
				temp[j] = FMODUnity.RuntimeUtils.ToFMODVector(mesh.vertices[4 * i + j]);
			}
			geomVerts[i] = temp;
		}

		for (int i = 0; i < 6; ++i)
		{
			result = geometry.addPolygon(0.2f, 0.0f, true, 4, geomVerts[i], out polygons[i]);
			Debug.Log(result);
		}
	}
	

	void Update ()
	{
		FMOD.VECTOR tempPos = FMODUnity.RuntimeUtils.ToFMODVector(transform.position);
		geometry.setPosition(ref tempPos);

		FMOD.VECTOR pos;
		geometry.getPosition(out pos);
		Debug.Log(pos.x);
	}
}
