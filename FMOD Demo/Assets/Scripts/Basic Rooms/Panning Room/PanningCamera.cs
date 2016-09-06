using UnityEngine;
using System.Collections;

public class PanningCamera : MonoBehaviour
{
    Camera m_follow;

	void Start () {
        m_follow = Camera.main;
	}
	void Update () {
        transform.position = m_follow.transform.position + Vector3.right * -40.0f;
        Camera cam = GetComponent<Camera>();
        cam.fieldOfView = m_follow.fieldOfView;
        cam.nearClipPlane = m_follow.nearClipPlane;
	}
}