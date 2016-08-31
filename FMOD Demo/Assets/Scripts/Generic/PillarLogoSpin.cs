using UnityEngine;


public class PillarLogoSpin : MonoBehaviour 
{
	
	void Update () 
	{
        transform.Rotate(Vector3.up, Time.deltaTime * 4);
	}

	#region Private Functions

	#endregion
}
