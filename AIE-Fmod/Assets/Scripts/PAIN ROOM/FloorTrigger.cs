using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour
{
	public GameObject falseFloor;
	public bool triggered = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (triggered)
		{
			if (falseFloor.transform.position.y > -2.5f)
			{
				falseFloor.transform.position -= new Vector3(0, 0.2f, 0);
			}
			else
				Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			triggered = true;
			
		}
	}
}
