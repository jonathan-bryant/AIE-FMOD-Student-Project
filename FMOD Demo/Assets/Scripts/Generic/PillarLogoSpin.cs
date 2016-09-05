using UnityEngine;


public class PillarLogoSpin : MonoBehaviour 
{
    public float m_speed = -4.0f;
	void Update () 
	{
        transform.Rotate(Vector3.up, Time.deltaTime * m_speed);
	}

	#region Private Functions

	#endregion
}
