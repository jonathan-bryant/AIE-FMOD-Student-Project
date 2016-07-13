// Made to replace unity standard listener with FMOD Studio Listener

using UnityEngine;
using System.Collections;

public class FMODCamera : MonoBehaviour
{	
	GameObject mainCamera;

	// Use this for initialization
	void Start ()
	{
		mainCamera = GameObject.Find("Main Camera");
		Destroy( mainCamera.GetComponent<AudioListener>() );
		mainCamera.AddComponent<FMODUnity.StudioListener>();
	}
}
