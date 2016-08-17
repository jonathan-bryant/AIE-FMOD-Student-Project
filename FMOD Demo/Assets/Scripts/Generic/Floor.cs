using UnityEngine;
using System.Collections;

public class Floor : ActionObject
{
	void Start () {
        m_description = "This is a floor";
        m_descriptionIs3D = true;
        m_descriptionPosition = transform.position + new Vector3(0.0f, 4.0f, 0.0f);
	}
	void Update () {
	
	}
}
