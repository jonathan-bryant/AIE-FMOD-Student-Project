using UnityEngine;
using System.Collections;

public class PanningCamera : MonoBehaviour
{
    Camera m_follow;
    public float m_distance;

	void Start () {
        m_follow = Camera.main;
	}
	void Update () {
        transform.position = m_follow.transform.position + Vector3.right * -m_distance;
        Camera cam = GetComponent<Camera>();
        cam.fieldOfView = m_follow.fieldOfView;
        cam.nearClipPlane = m_follow.nearClipPlane;
        cam.transform.eulerAngles = m_follow.transform.eulerAngles;
	}
}