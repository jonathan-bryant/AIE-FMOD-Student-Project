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
		mesh = new Mesh();
		mesh = GetComponent<MeshFilter>().mesh;
		numVerts = mesh.vertexCount;
		
		geomVerts = new FMOD.VECTOR[6][];
		result = FMODUnity.RuntimeManager.LowlevelSystem.createGeometry(numPolygons, numVerts, out geometry);
		
		// Geometry polygons have to be on the same plane, or things will break
		for (int i = 0; i < 6; ++i)
		{
			FMOD.VECTOR[] temp = new FMOD.VECTOR[4];
			//for (int j = 0; j < 4; ++j)
			//{
			//	//temp[j] = FMODUnity.RuntimeUtils.ToFMODVector(mesh.vertices[4 * i + j]);
			//}

			// Had to swap the 3rd and 4th verts so that the whole face of the prism, instead of in triangles
			Vector3 scale = transform.localScale;
			Debug.Log(scale);
			
			temp[0] = FMODUnity.RuntimeUtils.ToFMODVector(MultiplyByScale(mesh.vertices[4 * i + 0], scale));
			temp[1] = FMODUnity.RuntimeUtils.ToFMODVector(MultiplyByScale(mesh.vertices[4 * i + 1], scale));
			temp[2] = FMODUnity.RuntimeUtils.ToFMODVector(MultiplyByScale(mesh.vertices[4 * i + 3], scale));
			temp[3] = FMODUnity.RuntimeUtils.ToFMODVector(MultiplyByScale(mesh.vertices[4 * i + 2], scale));


			geomVerts[i] = temp;
			
			result = geometry.addPolygon(1.0f, 1.0f, true, 4, geomVerts[i], out polygons[i]);
		}
	}
	

	void Update ()
	{
		FMOD.VECTOR tempPos = FMODUnity.RuntimeUtils.ToFMODVector(transform.position);
		geometry.setPosition(ref tempPos);
		
		FMOD.VECTOR pos;
		result = geometry.getPosition(out pos);

		FMOD.VECTOR vertexInfo;
		for (int i = 0; i < 4; i++)
		{
			geometry.getPolygonVertex(0, i, out vertexInfo);
		}
	}

	Vector3 MultiplyByScale(Vector3 a_vec, Vector3 a_scale)
	{
		Vector3 result = a_vec;
		result.x *= a_scale.x;
		result.y *= a_scale.y;
		result.z *= a_scale.z;
		return result;
	}
}
