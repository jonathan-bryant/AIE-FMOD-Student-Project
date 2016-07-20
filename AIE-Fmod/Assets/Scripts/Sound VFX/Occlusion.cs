using UnityEngine;
using System.Collections;

public class Occlusion : MonoBehaviour
{
	[Range(0, 1.0f)]
	public float directOcclusion = 0.5f;
	[Range(0, 1.0f)]
	public float reverbOcclusion = 0.0f;

	FMOD.Geometry geometry;
	int numPolygons = 6;
	int numVerts;
	FMOD.VECTOR[][] geomVerts;
	int[] polygons = new int[6];

	Mesh mesh;
	Material mat;

	FMOD.RESULT result;

	void Start ()
	{
		mesh = new Mesh();
		mesh = GetComponent<MeshFilter>().mesh;
		numVerts = mesh.vertexCount;
		mat = GetComponent<Renderer>().material;
		
		geomVerts = new FMOD.VECTOR[6][];
		result = FMODUnity.RuntimeManager.LowlevelSystem.createGeometry(numPolygons, numVerts, out geometry);

		Vector3 scale = transform.localScale;
		
		// Geometry polygons have to be on the same plane, or things will break
		for (int i = 0; i < 6; ++i)
		{
			FMOD.VECTOR[] temp = new FMOD.VECTOR[4];

			temp[0] = FMODUnity.RuntimeUtils.ToFMODVector(mesh.vertices[4 * i + 0]);
			temp[1] = FMODUnity.RuntimeUtils.ToFMODVector(mesh.vertices[4 * i + 1]);
			temp[2] = FMODUnity.RuntimeUtils.ToFMODVector(mesh.vertices[4 * i + 3]);
			temp[3] = FMODUnity.RuntimeUtils.ToFMODVector(mesh.vertices[4 * i + 2]);

			geomVerts[i] = temp;
			
			result = geometry.addPolygon(directOcclusion, reverbOcclusion, true, 4, geomVerts[i], out polygons[i]);
		}
		FMOD.VECTOR scaleF = FMODUnity.RuntimeUtils.ToFMODVector(scale);
		geometry.setScale(ref scaleF);
	}
	

	void Update ()
	{
		FMOD.VECTOR tempPos = FMODUnity.RuntimeUtils.ToFMODVector(transform.position);
		geometry.setPosition(ref tempPos);
		
		FMOD.VECTOR pos;
		result = geometry.getPosition(out pos);

		for (int i = 0; i < 6; i++)
		{
			geometry.setPolygonAttributes(i, directOcclusion, reverbOcclusion, true);
			Color color = mat.color;
			color.a = directOcclusion;
			mat.color = color;
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
