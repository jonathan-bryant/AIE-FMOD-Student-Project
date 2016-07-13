using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += transform.forward * 0.1f;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position -= transform.forward * 0.1f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(new Vector3(0, 1, 0), -5);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(new Vector3(0, 1, 0), 5);
		}
	}
}
