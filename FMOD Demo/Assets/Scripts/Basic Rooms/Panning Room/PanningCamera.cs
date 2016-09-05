using UnityEngine;
using System.Collections;

public class PanningCamera : MonoBehaviour
{
    Transform m_follow;

	void Start () {
        m_follow = Camera.main.transform;
	}
	void Update () {
        transform.position = m_follow.position + Vector3.right * -40.0f;
	}
}