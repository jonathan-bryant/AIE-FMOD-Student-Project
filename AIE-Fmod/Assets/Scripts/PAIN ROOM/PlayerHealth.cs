using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int health = 50;
	public FMODUnity.EmitterRef emitterRef;

	private float woundTImer = 0.5f;
	private float counter = 0.0f;
	

	void Start ()
	{

	}
	
	void Update ()
	{
		if (health <= 25)
		{
			emitterRef.Target.SetParameter("Intensity", 0.60f);
		}

		if (health < 0)
			health = 0;
	}

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.CompareTag("LAVA"))
		{
			//timer
			counter += Time.deltaTime;
			if (counter >= woundTImer)
			{
				health -= 2;
				counter = 0.0f;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.CompareTag("LAVA"))
		{
			health = 50;
			emitterRef.Target.SetParameter("Intensity", 0.40f);
		}
	}
}
