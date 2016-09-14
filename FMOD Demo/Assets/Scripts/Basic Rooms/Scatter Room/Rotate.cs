using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public Vector3 m_rotationPerSecond;
    public bool m_randomStart;
	
    void Start ()
    {
	    if(m_randomStart)
        {
            transform.Rotate(m_rotationPerSecond.normalized * Random.Range(45, 315));
        }
	}
	void Update ()
    {
        transform.Rotate(m_rotationPerSecond * Time.deltaTime);
	}
}